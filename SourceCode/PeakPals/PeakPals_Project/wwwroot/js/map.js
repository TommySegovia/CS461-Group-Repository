
import { getAreaData } from "/js/api.js";

export function initializeDynamicMapArea(lng, lat, name, id, mode) {

    let dynamicMap;
    if (mode === "3d") {
        dynamicMap = (window.dynamicMap = new maplibregl.Map({
            container: 'dynamic-map',
            zoom: 12,
            center: [lng, lat],
            pitch: 70,
            hash: true,
            style: {
                version: 8,
                sources: {
                    osm: {
                        type: 'raster',
                        tiles: ['https://a.tile.openstreetmap.org/{z}/{x}/{y}.png'],
                        tileSize: 1024,
                        attribution: '&copy; OpenStreetMap Contributors',
                        maxzoom: 19
                    },
                    // Use a different source for terrain and hillshade layers, to improve render quality
                    terrainSource: {
                        type: 'raster-dem',
                        tiles: ['https://s3.amazonaws.com/elevation-tiles-prod/terrarium/{z}/{x}/{y}.png'],
                        tileSize: 256,
                        minzoom: 0,
                        maxzoom: 14
                    },
                    hillshadeSource: {
                        type: 'raster-dem',
                        tiles: ['https://s3.amazonaws.com/elevation-tiles-prod/terrarium/{z}/{x}/{y}.png'],
                        tileSize: 256,
                        minzoom: 0,
                        maxzoom: 14
                    }
                },
                layers: [
                    {
                        id: 'osm',
                        type: 'raster',
                        source: 'osm'
                    },
                    {
                        id: 'hills',
                        type: 'hillshade',
                        source: 'hillshadeSource',
                        layout: { visibility: 'visible' },
                        paint: { 'hillshade-shadow-color': '#473B24' }
                    }
                ],
                terrain: {
                    source: 'terrainSource',
                    exaggeration: 0.14
                }
            },
            maxZoom: 18,
            maxPitch: 85
        }));
    }
    else {
        let zoom = 13;
        if (id == "1db1e8ba-a40e-587c-88a4-64f5ea814b8e") {
            zoom = 3;
        }
        dynamicMap = new maplibregl.Map({
            container: 'dynamic-map', // container id
            style: 'https://api.maptiler.com/maps/streets/style.json?key=UOg2RBrpGopXMv4mVlUW', // style URL
            center: [lng, lat], // starting position [lng, lat]
            zoom: zoom, // starting zoom
        })
    }



    dynamicMap.on('load', async () => {

        const image = await dynamicMap.loadImage('https://maplibre.org/maplibre-gl-js/docs/assets/custom_marker.png');
        // Add an image to use as a custom marker
        dynamicMap.addImage('custom-marker', image.data);

        //api call
        const areaData = await getAreaData(id);
        console.log(areaData);

        dynamicMap.setStyle('starting-area', {
            version: 8,
            sources: {
                'starting-area': {
                    'type': 'geojson',
                    'data': {
                        'type': 'FeatureCollection',
                        'features': [
                            {
                                'type': 'Feature',
                                'properties': {
                                    'description': name

                                },
                                'geometry': {
                                    'type': 'Point',
                                    'coordinates': [lng, lat]
                                }
                            },
                        ]
                    }
                }
            },
            layers: [],
            glyphs: "https://demotiles.maplibre.org/font/{fontstack}/{range}.pbf"
        });


        dynamicMap.on('style.load', function() {
            dynamicMap.addLayer({
                'id': 'starting-area',
                'type': 'symbol',
                'source': 'starting-area',
                'layout': {
                    'icon-image': 'custom-marker',
                    'icon-overlap': 'always',
                    'text-field': ['get', 'description'],
                    'text-variable-anchor': ['top', 'bottom', 'left', 'right'],
                    'text-radial-offset': 0.5,
                    'text-justify': 'auto',
                    'text-size': 20
    
                },
                'paint': {
                    'text-color': '#000000',
                }
            });
        });
        

        let geojson;

        // if children exist, then gather their data into geojson, if the points have different coordinates make a hull as well and create layers for both!
        if (areaData.area.children.length > 0) {
            geojson = {
                'type': 'FeatureCollection',
                'features': areaData.area.children.map(child => ({
                    'type': 'Feature',
                    'properties': {
                        'description': child.area_Name,
                        'totalClimbs': child.totalClimbs,
                        'id': child.uuid,
                    },
                    'geometry': {
                        'type': 'Point',
                        'coordinates': [child.metadata.lng, child.metadata.lat]
                    }
                }))
            };

            const hull = turf.convex(geojson);

            if (hull && id != "1db1e8ba-a40e-587c-88a4-64f5ea814b8e") {
                const bufferedHull = turf.buffer(hull, 0.5, { units: 'kilometers' });

                dynamicMap.addSource('hull', {
                    'type': 'geojson',
                    'data': bufferedHull
                });

                dynamicMap.addLayer({
                    'id': 'hull',
                    'type': 'fill',
                    'source': 'hull',
                    'layout': {},
                    'paint': {
                        'fill-color': '#088',
                        'fill-opacity': 0.16
                    }
                });
            }

            dynamicMap.addSource('places', {
                'type': 'geojson',
                'data': geojson
            });

            // Add layers showing the places and the geometry.
            dynamicMap.addLayer({
                'id': 'places',
                'type': 'symbol',
                'source': 'places',
                'layout': {
                    'icon-image': 'circle_11',
                    'icon-overlap': 'always',
                    'text-field': ['get', 'description'],
                    'text-variable-anchor': ['top', 'bottom', 'left', 'right'],
                    'text-radial-offset': 0.5,
                    'text-justify': 'auto',
                    'text-size': 20

                },
                'paint': {
                    'text-color': '#ff005a'
                }
            });
        }


        // Create a popup, but don't add it to the map yet.
        const popup = new maplibregl.Popup({
            closeButton: false,
            closeOnClick: false
        });

        dynamicMap.on('mouseenter', 'places', (e) => {
            // Change the cursor style as a UI indicator.
            dynamicMap.getCanvas().style.cursor = 'pointer';

            const coordinates = e.features[0].geometry.coordinates.slice();
            const description = e.features[0].properties.description;
            const displayCoords = e.features[0].geometry.coordinates;
            const displayTotalClimbs = e.features[0].properties.totalClimbs;

            // Ensure that if the map is zoomed out such that multiple
            // copies of the feature are visible, the popup appears
            // over the copy being pointed to.
            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
            }

            // Populate the popup and set its coordinates
            // based on the feature found.
            popup.setLngLat(coordinates).setHTML(`
                <div style="text-align: center; font-size: 16px; word-wrap: break-word;">
                    <strong style="font-size: 20px;">${description}</strong>
                    <br>
                    Total Climbs: ${displayTotalClimbs}
                    <br>
                    <p style="font-size: 10px; color: blue;">$</">${displayCoords}</p>
                </div>
            `).addTo(dynamicMap);
        });

        dynamicMap.on('mouseleave', 'places', () => {
            dynamicMap.getCanvas().style.cursor = '';
            popup.remove();
        });


        dynamicMap.on('click', 'places', (e) => {
            // depending on the place clicked, route the user to another area/climb page
            const id = e.features[0].properties.id;

            window.location.href = `/Locations/Areas/${id}`;

        });

        dynamicMap.addControl(new maplibregl.FullscreenControl());
        dynamicMap.addControl(new maplibregl.NavigationControl());


    });
}


export function initializeDynamicMapClimb(lng, lat, name, id, ancestors, mode) {

    let dynamicMap;
    if (mode === "3d") {
        dynamicMap = (window.dynamicMap = new maplibregl.Map({
            container: 'dynamic-map-climb',
            zoom: 20,
            center: [lng, lat],
            pitch: 70,
            hash: true,
            style: {
                version: 8,
                sources: {
                    osm: {
                        type: 'raster',
                        tiles: ['https://a.tile.openstreetmap.org/{z}/{x}/{y}.png'],
                        tileSize: 1024,
                        attribution: '&copy; OpenStreetMap Contributors',
                        maxzoom: 19
                    },
                    // Use a different source for terrain and hillshade layers, to improve render quality
                    terrainSource: {
                        type: 'raster-dem',
                        tiles: ['https://s3.amazonaws.com/elevation-tiles-prod/terrarium/{z}/{x}/{y}.png'],
                        tileSize: 256,
                        minzoom: 0,
                        maxzoom: 14
                    },
                    hillshadeSource: {
                        type: 'raster-dem',
                        tiles: ['https://s3.amazonaws.com/elevation-tiles-prod/terrarium/{z}/{x}/{y}.png'],
                        tileSize: 256,
                        minzoom: 0,
                        maxzoom: 14
                    }
                },
                layers: [
                    {
                        id: 'osm',
                        type: 'raster',
                        source: 'osm'
                    },
                    {
                        id: 'hills',
                        type: 'hillshade',
                        source: 'hillshadeSource',
                        layout: { visibility: 'visible' },
                        paint: { 'hillshade-shadow-color': '#473B24' }
                    }
                ],
                terrain: {
                    source: 'terrainSource',
                    exaggeration: 0.14
                }
            },
            maxZoom: 18,
            maxPitch: 85
        }));
    }
    else {
        dynamicMap = new maplibregl.Map({
            container: 'dynamic-map-climb', // container id
            style: 'https://api.maptiler.com/maps/streets/style.json?key=UOg2RBrpGopXMv4mVlUW', // style URL
            center: [lng, lat], // starting position [lng, lat]
            zoom: 20, // starting zoom
        })
    }

    dynamicMap.on('load', async () => {

        const image = await dynamicMap.loadImage('https://maplibre.org/maplibre-gl-js/docs/assets/custom_marker.png');
        // Add an image to use as a custom marker
        dynamicMap.addImage('custom-marker', image.data);

        //api call
        const nearbyClimbData = await getAreaData(ancestors);
        console.log(nearbyClimbData);

        dynamicMap.setStyle('starting-climb', {
            version: 8,
            sources: {
                'starting-climb': {
                    'type': 'geojson',
                    'data': {
                        'type': 'FeatureCollection',
                        'features': [
                            {
                                'type': 'Feature',
                                'properties': {
                                    'description': name

                                },
                                'geometry': {
                                    'type': 'Point',
                                    'coordinates': [lng, lat]
                                }
                            },
                        ]
                    }
                }
            },
            layers: [],
            glyphs: "https://demotiles.maplibre.org/font/{fontstack}/{range}.pbf"
        });

        dynamicMap.on('style.load', function() {
            dynamicMap.addLayer({
                'id': 'starting-climb',
                'type': 'symbol',
                'source': 'starting-climb',
                'layout': {
                    'icon-image': 'custom-marker',
                    'icon-overlap': 'always',
                    'text-field': ['get', 'description'],
                    'text-variable-anchor': ['top', 'bottom', 'left', 'right'],
                    'text-radial-offset': 0.5,
                    'text-justify': 'auto',
                    'text-size': 20
    
                },
                'paint': {
                    'text-color': '#000000',
                }
            });
        });
        

        let geojson;

        // if children exist, then gather their data into geojson, if the points have different coordinates make a hull as well and create layers for both!
        if (nearbyClimbData && nearbyClimbData.area && nearbyClimbData.area.climbs.length > 0) {
            geojson = {
                'type': 'FeatureCollection',
                'features': nearbyClimbData.area.climbs.map(climb => ({
                    'type': 'Feature',
                    'properties': {
                        'description': climb.name,
                        'id': climb.uuid,
                    },
                    'geometry': {
                        'type': 'Point',
                        'coordinates': [climb.metadata.lng + (Math.random() - 0.5) / 10000, climb.metadata.lat + (Math.random() - 0.5) / 10000]
                    }
                }))
            };

            console.log(geojson);

            const hull = turf.convex(geojson);

            if (hull) {
                const bufferedHull = turf.buffer(hull, 0.035, { units: 'kilometers' });

                dynamicMap.addSource('hull', {
                    'type': 'geojson',
                    'data': bufferedHull
                });

                dynamicMap.addLayer({
                    'id': 'hull',
                    'type': 'fill',
                    'source': 'hull',
                    'layout': {},
                    'paint': {
                        'fill-color': '#088',
                        'fill-opacity': 0.16
                    }
                });
            }

            dynamicMap.addSource('places', {
                'type': 'geojson',
                'data': geojson
            });

            // Add layers showing the places and the geometry.
            dynamicMap.addLayer({
                'id': 'places',
                'type': 'symbol',
                'source': 'places',
                'layout': {
                    'icon-image': 'circle_11',
                    'icon-overlap': 'always',
                    'text-field': ['get', 'description'],
                    'text-variable-anchor': ['top', 'bottom', 'left', 'right'],
                    'text-radial-offset': 0.5,
                    'text-justify': 'auto',
                    'text-size': 17

                },
                'paint': {
                    'text-color': '#ff005a'
                }
            });
        }


        // Create a popup, but don't add it to the map yet.
        const popup = new maplibregl.Popup({
            closeButton: false,
            closeOnClick: false
        });

        dynamicMap.on('mouseenter', 'places', (e) => {
            // Change the cursor style as a UI indicator.
            dynamicMap.getCanvas().style.cursor = 'pointer';

            const coordinates = e.features[0].geometry.coordinates.slice();
            const description = e.features[0].properties.description;
            const displayCoords = e.features[0].geometry.coordinates;


            // Ensure that if the map is zoomed out such that multiple
            // copies of the feature are visible, the popup appears
            // over the copy being pointed to.
            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
            }

            // Populate the popup and set its coordinates
            // based on the feature found.
            popup.setLngLat(coordinates).setHTML(`
                <div style="text-align: center; font-size: 16px; word-wrap: break-word;">
                    <strong style="font-size: 20px;">${description}</strong>
                    <br>
                    <p style="font-size: 10px; color: blue;">$</">${displayCoords}</p>
                </div>
            `).addTo(dynamicMap);
        });

        dynamicMap.on('mouseleave', 'places', () => {
            dynamicMap.getCanvas().style.cursor = '';
            popup.remove();
        });


        dynamicMap.on('click', 'places', (e) => {
            // depending on the place clicked, route the user to another area/climb page
            const id = e.features[0].properties.id;

            window.location.href = `/Locations/Climbs/${id}`;
        });

        dynamicMap.addControl(new maplibregl.FullscreenControl());
        dynamicMap.addControl(new maplibregl.NavigationControl());
    });
}
