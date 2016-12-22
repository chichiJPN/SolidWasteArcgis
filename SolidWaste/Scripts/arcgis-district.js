
var locator, map, routeTask, routeParams;
var stopSymbol, routeSymbol, lastStop;

require([
  "esri/map", "esri/tasks/locator",
  "esri/SpatialReference", "esri/graphic",
  "esri/symbols/SimpleLineSymbol", "esri/symbols/SimpleMarkerSymbol",
  "esri/symbols/Font", "esri/symbols/TextSymbol",
  "esri/geometry/Point", "esri/geometry/Extent",
  "esri/geometry/webMercatorUtils",
  "dojo/_base/array", "esri/Color",
  "dojo/number", "dojo/parser", "dojo/dom", "dojo/json", "dijit/registry",
  "esri/InfoTemplate", "esri/layers/FeatureLayer",
  "esri/geometry/Polygon", "esri/symbols/SimpleFillSymbol",
  "esri/toolbars/edit", "dojo/_base/event", "esri/toolbars/draw",
  "esri/tasks/RouteTask", "esri/tasks/RouteParameters", "esri/tasks/FeatureSet",
  "dijit/form/Button", "dijit/form/Textarea",
  "dijit/layout/BorderContainer", "dijit/layout/ContentPane", "dojo/domReady!"
], function (
  Map, Locator,
  SpatialReference, Graphic,
  SimpleLineSymbol, SimpleMarkerSymbol,
  Font, TextSymbol,
  Point, Extent,
  webMercatorUtils,
  arrayUtils, Color,
  number, parser, dom, JSON, registry,
  InfoTemplate,FeatureLayer,
  Polygon, SimpleFillSymbol,
  Edit, event, Draw,
  RouteTask, RouteParameters,FeatureSet
) {

    parser.parse();

    var x_coor = parseFloat($("#dataDiv").data('x_coor')),
        y_coor = parseFloat($("#dataDiv").data('y_coor')),
        search_xmin = parseFloat($("#dataDiv").data('search_xmin')),
        search_xmax = parseFloat($("#dataDiv").data('search_xmax')),
        search_ymin = parseFloat($("#dataDiv").data('search_ymin')),
        search_ymax = parseFloat($("#dataDiv").data('search_ymax')),
        obj_id = parseInt($("#dataDiv").data('obj_id')),
        maxExtent = null,
        countiesFeatureLayer = null,
        graphicsLayer = null,
        temp = null,
        url = "http://gis.devgateway.org/arcgis/rest/services/AD_Internal/Africa/MapServer/36/query?objectIds=" + obj_id;
    //url = "http://gis.devgateway.org/arcgis/rest/services/AD_Internal/Africa/MapServer/36/";
    //url ="https://services.arcgis.com/HuLaMAzbcrbWfOM8/ArcGIS/rest/services/Mauritius/FeatureServer/0";

    var drawing = {
        id: null,
        name: '',
        drawtoolbar: null,
        editToolbar: null,
        flag: false,
        searchflag: false,

        mode: null,
        activateEditToolBar: 
            function (graphic) {
                var tool = 0;
                tool = tool | Edit.MOVE;
                tool = tool | Edit.EDIT_VERTICES;
                //tool = tool | Edit.SCALE;
                tool = tool | Edit.ROTATE;
                //tool = tool | Edit.EDIT_TEXT;
                //specify toolbar options        
                var options = {
                    allowAddVertices: true,
                    allowDeleteVertices: true,
                    uniformScaling: true
                };
                console.log('edit activated!');

                switch (graphic.geometry.type) {
                    case 'point':
                        console.log(graphic.geometry);

                        if (typeof graphic.geometry.cache != 'undefined') {
                            console.log('setting cache id');
                            drawing.id = graphic.geometry.cache.id;
                        }
                        console.log(drawing.id);
                        break;
                }
                document.getElementById('btn-municipality-save').style.display = 'block';
                drawing.editToolbar.activate(tool, graphic, options);
            }
    };


    map = new Map("viewDiv", {
        basemap: "streets",
        center: [57.559, -20.112],
        zoom: 12
    });

    map.on("load", function () {

        //map.disablePan();
        //map.hideZoomSlider();
        //map.disableDoubleClickZoom();
        //map.disableScrollWheelZoom();
        //map.disableShiftDoubleClickZoom();
        //map.disableKeyboardNavigation();
        //map.disableRubberBandZoom();
        

        var infoTemplate = new InfoTemplate("Attributes", "${*}"),
        countiesFeatureLayer = new FeatureLayer(url,
                                                {
                                                    mode: FeatureLayer.MODE_SNAPSHOT,
                                                    outFields: ['*'],
                                                    //infoTemplate: infoTemplate,
                                                });

        

        countiesFeatureLayer.on('update-end', function () {
            graphicsLayer = countiesFeatureLayer.graphics;
        });

        map.addLayer(countiesFeatureLayer);

        // polygon.setCacheValue('potato', 69);

        // removes a polygon shape from map

        //var graphics = map.graphics.graphics;

        //for (var x = 0, length = graphics.length ; x < length; x++) {
        //    console.log(graphics[x]);
        //    if (graphics[x].geometry.type == 'polygon' && typeof graphics[x].geometry.cache != 'undefined' && typeof graphics[x].geometry.cache.potato != 'undefined') {
        //        map.graphics.graphics[x].visible = false;
        //    }
        //}

        drawing.editToolbar = new Edit(map);

        map.graphics.on("click", function (evt) {
            if (drawing.flag == false && drawing.searchflag == false) {
                event.stop(evt);
                //var graphic = evt.graphic;
                //map.graphics.clear();
                //map.graphics.add(graphic);
                drawing.activateEditToolBar(evt.graphic);
            }
        });

        drawing.drawtoolbar = new Draw(map);
        drawing.drawtoolbar.on("draw-end", function (evt) {
            var symbol;
            drawing.drawtoolbar.deactivate();
            map.showZoomSlider();

            switch (evt.geometry.type) {
                case "point":
                case "multipoint":
                    symbol = new SimpleMarkerSymbol();
                    break;
                case "polyline":
                    symbol = new SimpleLineSymbol();
                    break;
                default:
                    symbol = new SimpleFillSymbol();
                    break;
            }

            var graphic = new Graphic(evt.geometry, symbol);
            map.graphics.add(graphic);

            console.log('drawing mode is ' + drawing.mode);

            switch (drawing.mode) {
                case 'addroute':
                    drawing.flag = false;

                    console.log(evt.geometry);
                    var stringBoundary = JSON.stringify(evt.geometry.rings[0]),
                        options = {
                            url: '/ajax/AddRoute',
                            type: 'POST',
                            data: {
                                id: drawing.id,
                                boundaries: stringBoundary
                            }
                        };

                    $('#loading').fadeIn();
                    $.ajax(options).done(function (data) {
                        console.log('data is ');
                        console.log(data);
                        //var chosenoption = document.getElementById('municipality-selection'),
                        //    selectedOption = chosenoption.options[chosenoption.selectedIndex];

                        //selectedOption.setAttribute('data-boundaries', stringBoundary);
                        $('#loading').fadeOut();
                    });
                    break;
                case 'municipality':
                    document.getElementById('btn-municipality-save').style.display = 'block';
                    drawing.flag = false;


                    var stringBoundary = JSON.stringify(evt.geometry.rings[0]),
                        options = {
                            url: '/ajax/updateMunicipalityBoundary',
                            type: 'POST',
                            data: {
                                id: drawing.id,
                                boundaries: stringBoundary
                            }
                        };

                    $('#loading').fadeIn();
                    $.ajax(options).done(function (data) {
                        console.log('data is ');
                        console.log(data);
                        var chosenoption = document.getElementById('municipality-selection'),
                            selectedOption = chosenoption.options[chosenoption.selectedIndex];

                        selectedOption.setAttribute('data-boundaries', stringBoundary);
                        $('#loading').fadeOut();
                    });
                    drawing.mode = null;


                    break;
                case 'addlorry':
                    console.log(evt.geometry);
                    var stringPoint = JSON.stringify([evt.geometry.x, evt.geometry.y]),
                        options = {
                            url: '/ajax/addlorry',
                            type: 'POST',
                            data: {
                                districtID:$('#dataDiv').data('district_id'),
                                boundaries: stringPoint,
                                name : drawing.name
                            }
                        };

                    drawing.name = '';
                    console.log(stringPoint);
                    $('#loading').fadeIn();

                    $.ajax(options).done(function (data) {
                        console.log('data is ');
                        console.log(data);
                        //selectedOption.setAttribute('data-boundaries', stringBoundary);
                        $('#loading').fadeOut();
                    });

                    drawing.mode = null;
                    break;
            }

        });

        map.on("click", showCoordinates);
    });
    // ##### DRAWING TOOLS ####

    // activates drawing feature
    function activateDrawingTool(shape) {
        //var tool = this.label.toUpperCase().replace(/ /g, "_");
        //drawing.drawtoolbar.activate('polygon');
        drawing.drawtoolbar.activate(shape);
        map.hideZoomSlider();
    }



    //##### address searching  ###

    locator = new Locator("https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer");

    //Draw and zoom to the result when the geocoding is complete
    locator.on("address-to-locations-complete", function (evt) {
        console.log(1);
        map.graphics.clear();
        arrayUtils.forEach(evt.addresses, function (geocodeResult, index) {
            //create a random color for the text and marker symbol
            var r = Math.floor(Math.random() * 250);
            var g = Math.floor(Math.random() * 100);
            var b = Math.floor(Math.random() * 100);

            var symbol = new SimpleMarkerSymbol(
              SimpleMarkerSymbol.STYLE_CIRCLE,
              20,
              new SimpleLineSymbol(
                SimpleLineSymbol.STYLE_SOLID,
                new Color([r, g, b, 0.5]),
                10
              ), new Color([r, g, b, 0.9]));
            var pointMeters = webMercatorUtils.geographicToWebMercator(geocodeResult.location);
            if (graphicsLayer[0].geometry.contains(pointMeters)) {
                //console.log(geocodeResult.attributes.PlaceName + ' is in the shape');
                var locationGraphic = new Graphic(pointMeters, symbol);
                var infoTemplate = new InfoTemplate(geocodeResult.attributes.PlaceName, "${*}");
                locationGraphic.setInfoTemplate(infoTemplate);
                var font = new Font().setSize("12pt").setWeight(Font.WEIGHT_BOLD);
                var textSymbol = new TextSymbol(
                  (index + 1) + ".) " + geocodeResult.address,
                  font,
                  new Color([r, g, b, 0.8])
                ).setOffset(5, 15);
                //add the location graphic and text with the address to the map
                map.graphics.add(locationGraphic);
                map.graphics.add(new Graphic(pointMeters, textSymbol));
            }
        });
        var ptAttr = evt.addresses[0].attributes;
        var minx = parseFloat(ptAttr.Xmin);
        var maxx = parseFloat(ptAttr.Xmax);
        var miny = parseFloat(ptAttr.Ymin);
        var maxy = parseFloat(ptAttr.Ymax);

        //var esriExtent = new Extent(minx, miny, maxx, maxy, new SpatialReference({ wkid: 4326 }));
        //console.log(esriExtent);
        //map.setExtent(webMercatorUtils.geographicToWebMercator(esriExtent));

        // showResults(evt.addresses);
    });
    // map.on("extent-change", updateExtent);

    function updateExtent() {
        console.log(2);
        /*
        dom.byId("currentextent").innerHTML = "<b>Current Extent JSON:</b> " + JSON.stringify(map.extent.toJson());
        dom.byId("currentextent").innerHTML += "<br/><b>Current Zoom level:</b> " + map.getLevel();
        */
    }

    function showResults(results) {
        console.log(3);
        /*
        var rdiv = dom.byId("resultsdiv");
        rdiv.innerHTML = "<p><b>Results : " + results.length + "</b></p>";

        var content = [];
        arrayUtils.forEach(results, function(result, index) {
          var x = result.location.x.toFixed(5);
          var y = result.location.y.toFixed(5);
          content.push("<fieldset>");
          content.push("<legend><b>" + (index + 1) + ". " + result.address + "</b></legend>");
          content.push("<i>Score:</i> " + result.score);
          content.push("<br/>");
          content.push("<i>Address Found In</i> : " + result.address);
          content.push("<br/><br/>");
          content.push("Latitude (y): " + y);
          content.push("&nbsp;&nbsp;");
          content.push("Longitude (x): " + x);
          content.push("<br/><br/>");
          content.push("<b>GeoRSS-Simple</b><br/>");
          content.push("&lt;georss:point&gt;" + y + " " + x + "&lt;/georss:point&gt;");
          content.push("<br/><br/>");
          content.push("<b>GeoRSS-GML</b><br/>");
          content.push("&lt;georss:where&gt;&lt;gml:Point&gt;&lt;gml:pos&gt;" + y + " " + x + "&lt;/gml:pos&gt;&lt;gml:Point&gt;&lt;/georss:where&gt;");
          content.push("<br/><br/>");
          content.push("<b>Esri JSON</b><br/>");
          content.push("<b>WGS:</b> " + JSON.stringify(result.location.toJson()));
          content.push("<br/>");

          var location_wm = webMercatorUtils.geographicToWebMercator(result.location);

          content.push("<b>WM:</b> " + JSON.stringify(location_wm.toJson()));
          content.push("<br/><br/>");
          content.push("<b>Geo JSON</b><br/>");
          content.push('"geometry": {"type": "Point", "coordinates": [' + y + ',' + x + ']}');
          content.push("<br/><br/>");
          content.push("<input type='button' value='Center At Address' onclick='zoomTo(" + y + "," + x + ")'/>");
          content.push("</fieldset>");
        });
        rdiv.innerHTML += content.join("");
        */
    }

    document.getElementById('locate').onclick = locate;
    document.getElementById('foo').onsubmit = locate;

    //Perform the geocode. This function runs when the "Locate" button is pushed.
    function locate() {
        drawing.searchflag = true;
        var address = {
            //SingleLine: dom.byId("address").value,
            address: dom.byId("address").value,
        };
        console.log(address);

        var options = {
            address: address,
            //			countryCode:'IN',
            outFields: ["*"],
            searchExtent: new Extent({
                "spatialReference": {
                    "wkid": 4326
                },
                "xmin": search_xmin,
                "xmax": search_xmax,
                "ymin": search_ymin,
                "ymax": search_ymax
            })
        };
        //optionally return the out fields if you need to calculate the extent of the geocoded point
        locator.addressToLocations(options);
        return false;
    }
    // #### END OF ADDRESS SEARCHING

    // MISCELLANEOUS
    function showCoordinates(evt) {
        //the map is in web mercator but display coordinates in geographic (lat, long)
        var mp = webMercatorUtils.webMercatorToGeographic(evt.mapPoint);
        //display mouse coordinates
        console.log(mp.x.toFixed(3) + ", " + mp.y.toFixed(3));
        drawing.editToolbar.deactivate();
        document.getElementById('btn-municipality-save').style.display = 'none';
    }
    // END OF MISCELLANIOUS

    // MUNICIPALITY EDITTING
    document.getElementById('municipality-selection').onchange = municipalitySelect;
    function municipalitySelect() {
        map.graphics.clear();
        drawing.drawtoolbar.deactivate();
        drawing.editToolbar.deactivate();
        drawing.flag = false;
        drawing.searchflag = false;
        var selected = this.options[this.selectedIndex],
            div_instruct = document.getElementById('instructions');

        div_instruct.innerHTML = '';

        if (selected.getAttribute('data-id')) {
            drawing.id = selected.getAttribute('data-id');

            if (!selected.getAttribute('data-boundaries') || selected.getAttribute('data-boundaries') == null || selected.getAttribute('data-boundaries') == "") {

                var p = document.createElement('p');
                p.innerHTML = 'Boundaries were not set for this municipality. Please click on the map and create boundaries for this municipality';
                div_instruct.appendChild(p);
                drawing.flag = true;
                drawing.mode = 'municipality';
                activateDrawingTool('polygon');
                
            } else {
                // there are boundaries
                var polygonSymbol = new SimpleFillSymbol(),
                    boundaries = JSON.parse(selected.getAttribute('data-boundaries'));

                var polygon = new Polygon({
                    "rings": [boundaries],
                    "spatialReference": {
                        "wkid": 102100
                    }
                });

                var p = document.createElement('p');
                p.innerHTML = 'You can edit the boundaries by clicking on the shape. Click on the save button to save your changes or click on the map to cancel';
                div_instruct.appendChild(p);

                map.graphics.add(new Graphic(polygon, polygonSymbol));
            }
        }
    }

    document.getElementById('btn-addlorries').onclick = addlorries;
    function addlorries() {
        drawing.mode = 'addlorry';
        $('#modalBody').html('<input type="text" id="addlorry_name" placeholder="Name of lorry" class="form-control">');
        $('#modalFooter').html(''+
            '<button id="btn-addlorries-execute" type="button" class="btn btn-secondary">Add</button>'+
            '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>');
        

        $('#btn-addlorries-execute').click(function () {
            drawing.searchflag = false;

            drawing.name = $('#addlorry_name').val();
            $('#theModal').modal('hide');
            activateDrawingTool('point');
        });

        $('#theModal').modal('show');
    }

    document.getElementById('btn-addroute').onclick = addroute;

    function addroute() {
        drawing.searchflag = false;
        drawing.mode = 'addroute';
        $('#modalBody').html('<input type="text" id="addroutename" placeholder="Name of route" class="form-control">');
        $('#modalFooter').html('' +
            '<button id="btn-addroute-execute" type="button" class="btn btn-secondary">Add</button>' +
            '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>');


        $('#btn-addroute-execute').click(function () {
            drawing.searchflag = false;
            drawing.flag = true;
            drawing.name = $('#addlorry_name').val();
            $('#theModal').modal('hide');
            activateDrawingTool('polyline');
        });

        $('#theModal').modal('show');

    }
    
    document.getElementById('btn-viewroutes').onclick = viewroutes;
    function viewroutes() {
        console.log('view routes clicked');
        drawing.searchflag = false;
        var options = {
            url: '/ajax/GetRoutes',
            type: 'POST',
            data: {
                districtID: $('#dataDiv').data('district_id')
            }
        };

        $('#loading').fadeIn();
        $.ajax(options).done(function (data) {
            map.graphics.clear();

            for (var x = 0, length = data.length ; x < length; x++) {
                var boundaries = JSON.parse(data[x].boundaries),
                    symbol = new SimpleMarkerSymbol(),
                    geometry = new Point(boundaries[0], boundaries[1], new SpatialReference({ wkid: 102100 }));

                geometry.setCacheValue('id', data[x].id);

                var graphic = new Graphic(geometry, symbol);
                console.log(graphic);
                map.graphics.add(graphic);
            }
            $('#loading').fadeOut();
        });


    }

    document.getElementById('btn-togglelorries').onclick = togglelorries;
    function togglelorries() {

        drawing.searchflag = false;
        var options = {
                url: '/ajax/GetLorries',
                type: 'POST',
                data: {
                    districtID: $('#dataDiv').data('district_id')
                }
            };

        $('#loading').fadeIn();
        $.ajax(options).done(function (data) {
            map.graphics.clear();

            for (var x = 0, length = data.length ; x < length; x++) {
                var boundaries = JSON.parse(data[x].boundaries),
                    symbol = new SimpleMarkerSymbol(),
                    geometry = new Point(boundaries[0], boundaries[1], new SpatialReference({ wkid: 102100 }));

                    geometry.setCacheValue('id', data[x].id);

                var graphic = new Graphic(geometry, symbol);
                        console.log(graphic);
                    map.graphics.add(graphic);
            }
            $('#loading').fadeOut();
        });
    }

    document.getElementById('btn-municipality-save').onclick = municipalitySaveChanges;
    function municipalitySaveChanges() {
        // save code here
        console.log('save changes');
        var graphic = drawing.editToolbar.getCurrentState().graphic;

        this.style.display = 'none';
        switch (graphic.geometry.type) {
            case 'point':
                console.log('point');
                console.log('updating lorry id ' + drawing.id);
                var stringBoundary = JSON.stringify([graphic.geometry.x, graphic.geometry.y]),
                    options = {
                        url: '/ajax/updateLorryBoundary',
                        type: 'POST',
                        data: {
                            id: drawing.id,
                            boundaries: stringBoundary
                        }
                    };

                $('#loading').fadeIn();
                $.ajax(options).done(function (data) {

                    drawing.id = null;

                    var chosenoption = document.getElementById('municipality-selection'),
                        selectedOption = chosenoption.options[chosenoption.selectedIndex];

                    selectedOption.setAttribute('data-boundaries', stringBoundary);

                    drawing.editToolbar.deactivate();
                    $('#loading').fadeOut();
                });

                break;
            case 'polygon':
                var stringBoundary = JSON.stringify(graphic.geometry.rings[0]),
                    options = {
                        url: '/ajax/updateMunicipalityBoundary',
                        type: 'POST',
                        data: {
                            id: drawing.id,
                            boundaries: stringBoundary
                        }
                    };

                $('#loading').fadeIn();
                $.ajax(options).done(function (data) {

                    drawing.id = null;

                    var chosenoption = document.getElementById('municipality-selection'),
                        selectedOption = chosenoption.options[chosenoption.selectedIndex];

                    selectedOption.setAttribute('data-boundaries', stringBoundary);

                    drawing.editToolbar.deactivate();
                    $('#loading').fadeOut();
                });
                break;
        }
        drawing.editToolbar.deactivate();
    }
});

function zoomTo(lat, lon) {
    console.log(5);

    require([
      "esri/geometry/Point", "esri/geometry/webMercatorUtils"
    ], function (Point, webMercatorUtils) {
        var point = new Point(lon, lat, {
            wkid: "4326"
        });
        var wmpoint = webMercatorUtils.geographicToWebMercator(point);
        map.centerAt(wmpoint);
    });
}