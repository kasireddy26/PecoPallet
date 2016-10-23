'use strict';
PecoApp.factory('mapsFactory', function () {

    var pushpincreate = function() {
        return {
            Id: 0,
            Name: null,
            Latitude: null,
            Longitude: null,
            Zip: 0
        };
    }
    var pushpinvalidate = function (data) {
        return null;
    }

	var polygoncreate = function() {
        return {
            Id: 0,
            Name: null,            
            Zip: 0,
			Points: [{
				Pid: 0,
				Latitude: null,
				Longitude: null
			}]
        };
    }
	
    return {
        PushpinCreate: function () {
            return pushpincreate();
        },
        PushpinValidate: function(data) {
            return pushpinvalidate(data);
        },
		PolygonCreate: function () {
            return polygoncreate();
        }		
		
    }

});
