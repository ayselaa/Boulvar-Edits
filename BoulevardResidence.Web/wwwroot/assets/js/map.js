var map = new google.maps.Map(document.getElementById("map"), {
    center: { lat: 40.34210, lng: 49.834070 },
    zoom: 25,
    minZoom: 0,
    styles: 
    [
      {
          "featureType": "water",
          "elementType": "geometry",
          "stylers": [
              {
                  "color": "#e9e9e9"
              },
              {
                  "lightness": 17
              }
          ]
      },
      {
          "featureType": "landscape",
          "elementType": "geometry",
          "stylers": [
              {
                  "color": "#f5f5f5"
              },
              {
                  "lightness": 20
              }
          ]
      },
      {
          "featureType": "road.highway",
          "elementType": "geometry.fill",
          "stylers": [
              {
                  "color": "#ffffff"
              },
              {
                  "lightness": 17
              }
          ]
      },
      {
          "featureType": "road.highway",
          "elementType": "geometry.stroke",
          "stylers": [
              {
                  "color": "#ffffff"
              },
              {
                  "lightness": 29
              },
              {
                  "weight": 0.2
              }
          ]
      },
      {
          "featureType": "road.arterial",
          "elementType": "geometry",
          "stylers": [
              {
                  "color": "#ffffff"
              },
              {
                  "lightness": 18
              }
          ]
      },
      {
          "featureType": "road.local",
          "elementType": "geometry",
          "stylers": [
              {
                  "color": "#ffffff"
              },
              {
                  "lightness": 16
              }
          ]
      },
      {
          "featureType": "poi",
          "elementType": "geometry",
          "stylers": [
              {
                  "color": "#f5f5f5"
              },
              {
                  "lightness": 21
              }
          ]
      },
      {
          "featureType": "poi.park",
          "elementType": "geometry",
          "stylers": [
              {
                  "color": "#dedede"
              },
              {
                  "lightness": 21
              }
          ]
      },
      {
          "elementType": "labels.text.stroke",
          "stylers": [
              {
                  "visibility": "on"
              },
              {
                  "color": "#ffffff"
              },
              {
                  "lightness": 16
              }
          ]
      },
      {
          "elementType": "labels.text.fill",
          "stylers": [
              {
                  "saturation": 36
              },
              {
                  "color": "#333333"
              },
              {
                  "lightness": 40
              }
          ]
      },
      {
          "elementType": "labels.icon",
          "stylers": [
              {
                  "visibility": "off"
              }
          ]
      },
      {
          "featureType": "transit",
          "elementType": "geometry",
          "stylers": [
              {
                  "color": "#f2f2f2"
              },
              {
                  "lightness": 19
              }
          ]
      },
      {
          "featureType": "administrative",
          "elementType": "geometry.fill",
          "stylers": [
              {
                  "color": "#fefefe"
              },
              {
                  "lightness": 20
              }
          ]
      },
      {
          "featureType": "administrative",
          "elementType": "geometry.stroke",
          "stylers": [
              {
                  "color": "#fefefe"
              },
              {
                  "lightness": 17
              },
              {
                  "weight": 1.2
              }
          ]
      }
  ]
  });
  
  var markers = [
    new google.maps.Marker({
      id: 1,
      position: { lat: 40.342100, lng: 49.834010 },
      title: "School",
      map: map,
      category: "school",
      icon: {
        url: "/assets/svg/school.svg",
        scaledSize: new google.maps.Size(40, 40),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(20, 40),
      },
    }),
    new google.maps.Marker({
      id: 2,
      position: { lat: 40.3421220, lng: 49.834015 },
      title: "Gym",
      map: map,
      category: "gym",
      icon: {
        url: "/assets/svg/gym.svg",
        scaledSize: new google.maps.Size(40, 40),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(20, 40),
      },
    }),
    new google.maps.Marker({
      id: 3,
      position: { lat: 40.342120, lng: 49.83406 },
      title:
        "park",
      map: map,
      category: "park",
      icon: {
        url: "/assets/svg/park.svg",
        scaledSize: new google.maps.Size(40, 40),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(20, 40),
      },
    }),
    new google.maps.Marker({
      id: 4,
      position: { lat: 40.342100, lng: 49.834029 },
      title:
        "pharmacy",
      map: map,
      category: "pharmacy",
      icon: {
        url: "/assets/svg/pharmacy.svg",
        scaledSize: new google.maps.Size(40, 40),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(20, 40),
      },
    }),
    new google.maps.Marker({
      id: 5,
      position: { lat: 40.342106, lng: 49.834050 },
      title: "restuarant",
      map: map,
      category: "restuarant",
      icon: {
        url: "/assets/svg/resturarant.svg",
        scaledSize: new google.maps.Size(40, 40),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(20, 40),
      },
    }),
    new google.maps.Marker({
      id: 6,
      position: { lat: 40.342130, lng: 49.834029 },
      title: "playground",
      map: map,
      category: "playground",
      icon: {
        url: "/assets/svg/playground.svg",
        scaledSize: new google.maps.Size(40, 40),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(20, 40),
      },
    }),
    new google.maps.Marker({
      id: 7,
      position: { lat: 40.342130, lng: 49.834045 },
      title: "supermarket",
      map: map,
      category: "supermarket",
      icon: {
        url: "/assets/svg/supermarket.svg",
        scaledSize: new google.maps.Size(40, 40),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(20, 40),
      },
    }),
  ];
  
  
  var markerCategories = {
    "school": [],
    "gym": [],
    "park": [],
    "pharmacy": [],
    "supermarket": [],
    "playground": [],
    "restuarant": []
  };
  
  markers.forEach(function(marker) {
    markerCategories[marker.category].push(marker);
  });
  
  function updateMarkers() {
    console.log("UpdateMarkers function called.");
    for (var category in markerCategories) {
      var checkbox = document.querySelector('input[value="' + category + '"]');
      var isChecked = checkbox.checked;
    
      markerCategories[category].forEach(function(marker) {
        marker.setMap(isChecked ? map : null);
      });
    }
  }
  
  document.querySelectorAll('.chk-btn').forEach(function(checkbox) {
    checkbox.addEventListener('change', updateMarkers);
  });
  
  updateMarkers();
  

