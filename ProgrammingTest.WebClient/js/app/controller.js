app.controller('memberLister',
    ['$scope', 'members', function ($scope, members) { // load the service we created in members.js.

        // init all of the scope variables used in the markup.
        $scope.Members      = [];
        $scope.NewMember    = {};
        $scope.Busy         = false;
        $scope.FilterString = '';

        // define refresh members function on the scope so it can be called from the view.
        $scope.RefreshMembersList = function () {
            
            // to be consumed by the markup so certain elements can be disabled while requests are in progress.
            $scope.Busy = true; 

            members.list().then(function (response) {
                $scope.Members = response.data;
                $scope.Busy = false;
            }).catch(function (err) {
                $scope.Busy = false;
                // could consume Controller ModelState validation error info here...
            });
        };

        // define add member function on the scope so it can be called from the view.
        $scope.AddMember = function (member) {
            
            // to be consumed by the markup so certain elements can be disabled while requests are in progress.
            $scope.Busy = true;
            
            // proactively insert the row - makes the app looks faster :)
            $scope.Members.push(member); 

            members.create(member).then(function (response) {
                $scope.Busy = false;
                
                // remove and re-add the record, it will have an ID and created date now.
                $scope.Members.pop(); 
                $scope.Members.push(response.data);
                
                // clear the object used by the page form.
                $scope.NewMember = {};
            }).catch(function (err) {
                $scope.Members.pop(); // remove it only on failure.
                $scope.Busy = false;
                // could consume Controller ModelState validation error info here...
            })
        };

        // kick off an initial refresh so the markup can be updated.
        $scope.RefreshMembersList();
    }]);
