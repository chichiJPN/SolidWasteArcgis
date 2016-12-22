var map;

require(["esri/map",
    "esri/layers/FeatureLayer",
    "esri/tasks/query",
    "esri/geometry/Geometry",
    "esri/geometry/Point",
    "esri/geometry/Polyline",
    "esri/geometry/Polygon",
    "esri/graphic",
    "esri/symbols/PictureMarkerSymbol",
    "esri/symbols/SimpleMarkerSymbol",
    "esri/symbols/SimpleLineSymbol",
    "esri/symbols/SimpleFillSymbol",
    "esri/Color",
    "esri/InfoTemplate",
    "esri/geometry/webMercatorUtils",
    "esri/symbols/TextSymbol",
    "esri/symbols/Font",
    "dojo/domReady!",
    "esri/geometry"], function (Map,
FeatureLayer,
query,
Geometry,
Point,
Polyline,
Polygon,
Graphic,
PictureMarkerSymbol,
SimpleMarkerSymbol,
SimpleLineSymbol,
SimpleFillSymbol,
Color,
InfoTemplate,
webMercatorUtils,
TextSymbol,
Font) {

    map = new Map("viewDiv", {
        basemap: "satellite",
        center: [57.6, -20.25],
        zoom: 10
    });

    
    map.on("load", function () {
        map.disablePan();
        map.hideZoomSlider();
        map.disableDoubleClickZoom();
        map.disableScrollWheelZoom();
        map.disableShiftDoubleClickZoom();
        map.disableKeyboardNavigation();
        map.disableRubberBandZoom();

        //map.addLayer(countiesFeatureLayer);

        var options = {
            url: $('#dataDiv').data('district-url'),
            type: 'POST'
        };

        $.ajax(options).done(function (data) {
            console.log('districts are ');
            console.log(data);

            var pointSymbol = new PictureMarkerSymbol('../Images/bluemarker.png', 60, 60),
                graphicsArray = [];

            for (var x = 0; x < data.length; x++) {
                var district = data[x], // passes the retrieved data array to temporary variable 'dstrict'
                    newGraphics = new Graphic( // creates the marker
                                    new Point(district['x_coor'], district['y_coor']),
                                    pointSymbol,
                                    { district: district['name'] }
                                ).setInfoTemplate(
                                    (new InfoTemplate(district['name'])).setContent('<a href="' + window.baseUrl + 'Member/District?name=' + district['name'] + '&id=' + district['id'] + '" target="_blank"> Click here to know more</a>')
                                );


                var textSymbol = new esri.Graphic(
                                    new esri.geometry.Point(district['x_coor'], district['y_coor'] - 0.0500),
                                    new esri.symbol.TextSymbol(district['name'])
                                        .setColor(
                                            new dojo.Color([256, 256, 256])
                                        )
                                        .setAlign(esri.symbol.TextSymbol.ALIGN_MIDDLE)
                                        .setFont(
                                            new esri.symbol.Font("12pt")
                                                .setWeight(esri.symbol.Font.WEIGHT_BOLD)
                                        )
                                );

                graphicsArray.push({
                    marker: newGraphics,
                    text: textSymbol
                });

            }

            var infoTemplate = new InfoTemplate("Attributes", "${*}"),
                countiesFeatureLayer = new FeatureLayer("http://gis.devgateway.org/arcgis/rest/services/AD_Internal/Africa/MapServer/36",
                                            {
                                                mode: FeatureLayer.MODE_ONDEMAND,
                                                outFields: ['*'],
                                                //infoTemplate: infoTemplate,
                                                showLabels: true
                                            });

            for (i = 0; i < graphicsArray.length; ++i) {
                map.graphics.add(graphicsArray[i]['marker']);
                map.graphics.add(graphicsArray[i]['text']);
            }

            console.log(4);

            //addText(57.559, -20.077, "helllasdasdasdssz");
            //after map loads, connect to listen to mouse move & drag events
            map.on("mouse-move", showCoordinates);
            map.on("mouse-drag", showCoordinates);
            console.log('done');
        });
    });

    function showCoordinates(evt) {
        //the map is in web mercator but display coordinates in geographic (lat, long)
        var mp = webMercatorUtils.webMercatorToGeographic(evt.mapPoint);
        //display mouse coordinates
        console.log(mp.x.toFixed(3) + ", " + mp.y.toFixed(3));
    }
});
