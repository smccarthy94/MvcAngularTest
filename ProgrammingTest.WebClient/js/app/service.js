app.service('members', ['$http', function ($http) {
    return {
        list: function ()           { return $http.get('/api/members') },
        get: function (toGet)       { return $http.get('/api/members/' + toGet) }, // toGet, number
        delete: function (toDelete) { return $http.delete('/api/members/' + toDelete) }, // toDelete, number
        update: function (toUpdate) { return $http.put('/api/members', toUpdate) }, // toUpdate, object
        create: function (toCreate) { return $http.post('/api/members', toCreate) }, // toCreate, object
    }
}]);
