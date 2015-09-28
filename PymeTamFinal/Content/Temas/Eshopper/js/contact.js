// Google Map Customization
(function(){
    var opciones = function () {
        var lat;
        var long;
        $.get("/Tienda/Inicio/CargaCoords", function (d) {
            lat = d.lat;
            long = d.lng;
            var map;
            map = new GMaps({
                el: '#gmap',
                lat: d.lat,
                lng: d.lng,
                scrollwheel: false,
                zoom: 14,
                panControl: false,
                streetViewControl: false,
                mapTypeControl: false,
                overviewMapControl: false,
                clickable: true
            });

            var image = 'images/map-icon.png';
            map.addMarker({
                lat: lat,
                lng: long,
                // icon: image,
                animation: google.maps.Animation.DROP,
                verticalAlign: 'bottom',
                horizontalAlign: 'center',
                backgroundColor: '#ffffff',
            });

            var styles = [

            {
                "featureType": "road",
                "stylers": [
                { "color": "" }
                ]
            }, {
                "featureType": "water",
                "stylers": [
                { "color": "#A2DAF2" }
                ]
            }, {
                "featureType": "landscape",
                "stylers": [
                { "color": "#ABCE83" }
                ]
            }, {
                "elementType": "labels.text.fill",
                "stylers": [
                { "color": "#000000" }
                ]
            }, {
                "featureType": "poi",
                "stylers": [
                { "color": "#2ECC71" }
                ]
            }, {
                "elementType": "labels.text",
                "stylers": [
                { "saturation": 1 },
                { "weight": 0.1 },
                { "color": "#111111" }
                ]
            }

            ];

            map.addStyle({
                styledMapName: "Styled Map",
                styles: styles,
                mapTypeId: "map_style"
            });

            map.setStyle("map_style");
        });
    }
    opciones();
}());