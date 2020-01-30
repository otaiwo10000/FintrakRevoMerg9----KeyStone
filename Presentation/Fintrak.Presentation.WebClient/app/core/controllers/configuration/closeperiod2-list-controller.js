/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ClosePeriod2Controller",
            ['$scope', '$state', 'viewModelHelper', 'validator',
                ClosePeriod2Controller]);

    function ClosePeriod2Controller($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.view = 'closeperiod2-list-view';
        vm.viewName = 'Close Period';
        vm.returnValue = 0;

        vm.incomesetup = {};
        vm.p = "";

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.outputresult = 'not really';
        var initialize = function () {


        };


        vm.closeperiodFunc = function () {

            if (vm.init === false) {
                if (confirm("You are about to close period for" + "--" + vm.p + "--" + ". Click OK if you are sure. Otherwise, click cancel")) {
                    vm.viewModelHelper.apiPost('api/closeperiod2/updatecloseperiod2', null,
                        function (result) {
                            vm.returnValue = result.data;
                            //var vaa = vm.returnValue;

                            if (vm.returnValue.toString() === 1) { vm.outputresult = 'stored procedure successfully called'; }
                            else { vm.outputresult = 'something went wrong. Please, contact the system admin'; }

                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
            }
        };


        var latestincomesetupFunc = function () {
            vm.viewModelHelper.apiGet('api/incomesetup/latestincomesetup', null,
                function (result) {
                    vm.incomesetup = result.data;
                    periodFunc(vm.incomesetup.CurrentPeriod);
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };


      
        // angular.forEach(vm.MTList_t2, function (value, key) {
        var periodFunc = function (peri) {
            vm.p ="";
            switch (peri) {
                    case 1:
                        //vm.p.push("Jan");
                        vm.p = "January";
                        break;
                    case 2:
                        vm.p = "Febuary";
                        break;
                    case 3:
                        vm.p = "March";
                    break;
                    case 4:
                        vm.p = "April";
                        break;
                    case 5:
                        vm.p = "May";
                        break;
                    case 6:
                        vm.p = "June";
                        break;
                    case 7:
                        vm.p = "July";
                        break;
                    case 8:
                        vm.p = "August";
                        break;
                    case 9:
                        vm.p = "September";
                        break;
                    case 10:
                        vm.p = "October";
                        break;
                    case 11:
                        vm.p = "November";
                        break;                   
                    case 12:
                        vm.p = "December";
                }
        };


        latestincomesetupFunc();
        //initialize();
    }
}());
