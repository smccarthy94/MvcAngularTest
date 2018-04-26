app.controller('memberLister',
    ['$scope', 'members', function ($scope, members) {

        $scope.Members      = [];
        $scope.NewMember    = {};
        $scope.Busy         = false;
        $scope.FilterString = '';

        $scope.RefreshMembersList = function () {
            $scope.Busy = true;

            members.list().then(function (response) {
                $scope.Members = response.data;
                $scope.Busy = false;
            }).catch(function (err) {
                $scope.Busy = false;
                // could consume Controll ModelState validation error info here...
            });
        };

        $scope.AddMember = function (member) {

            $scope.Busy = true;
            $scope.Members.push(member); // pro-actively insert the row - makes the app looks faster :)

            members.create(member).then(function (response) {
                $scope.Busy = false;
                $scope.Members.pop(); //remove and re-add the record, it will have an ID and created data now.
                $scope.Members.push(response.data);
                $scope.NewMember = {};
            }).catch(function (err) {
                $scope.Members.pop(); // remove it only on failure
                $scope.Busy = false;
                // could consume Controll ModelState validation error info here...
            })
        };

        $scope.RefreshMembersList();
    }]);