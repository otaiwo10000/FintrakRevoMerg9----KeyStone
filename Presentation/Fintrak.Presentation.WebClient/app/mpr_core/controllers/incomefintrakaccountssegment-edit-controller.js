/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeFintrakAccountsSegmentEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeFintrakAccountsSegmentEditController]);

    function IncomeFintrakAccountsSegmentEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomefintrakaccountssegment-edit-view';
        vm.viewName = 'Accounts Segments';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifas = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ifasRules = [];

        var setupRules = function () {

            ifasRules.push(new validator.PropertyRule("Bank", {
                required: { message: "Bank is required" }
            }));

            ifasRules.push(new validator.PropertyRule("CUSTOMERID", {
                required: { message: "CUSTOMERID is required" }
            }));

        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.Id !== 0) {
                    vm.viewModelHelper.apiGet('api/incomefintrakaccountssegment/getincomefintrakaccountssegment/' + $stateParams.Id, null,
                        function (result) {
                            vm.ifas = result.data;

                            //initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    //vm.abcRatio = { Branch: '', Percentage: 0, Active: true };
                    vm.ifas = {
                        SegmentCode: '', AccoutofficerCode: '', CUSTOMERID: '', ACCOUNTNUMBER: '',
                        CUSTOMERNAME: '', TEAMNAME: '', Bank: '', Description: '', Active: true
                    };
            }
        };


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ifas, ifasRules);
            vm.viewModelHelper.modelIsValid = vm.ifas.isValid;
            vm.viewModelHelper.modelErrors = vm.ifas.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomefintrakaccountssegment/updateincomefintrakaccountssegment', vm.ifas,
                    function (result) {

                        $state.go('mpr-incomefintrakaccountssegment-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.ifas.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        };

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/incomefintrakaccountssegment/deleteincomefintrakaccountssegment', vm.ifas.Id,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-incomefintrakaccountssegment-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-incomefintrakaccountssegment-list');
        };

        setupRules();
        initialize();
    }
}());
