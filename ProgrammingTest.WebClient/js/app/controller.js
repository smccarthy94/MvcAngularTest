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
                
                // set the form in the view to $pristine state
                $scope.newForm.$setPristine();
                // this tells the form that it should appear as untouched, without this line all fields within
                // the form will show as being in an error state (as all fields are 'required' and have just been cleared).
                //
                // When the form is in an untouched state, it doesn't show 'required' error messages on fields
                // until a user either interacts with a field and intentionally leaves it blank or hit submit too early.
                //
                // Note that we can iteract with the form element via the $scope using the 'name' that was provided
                // on the form element in the view.
            }).catch(function (err) {
                $scope.Members.pop(); // remove it only on failure.
                $scope.Busy = false;
                // could consume Controller ModelState validation error info here...
            })
        };

        // kick off an initial refresh so the markup can be updated.
        $scope.RefreshMembersList();
    }]);
