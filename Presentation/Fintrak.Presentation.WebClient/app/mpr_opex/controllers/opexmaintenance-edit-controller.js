/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexMaintenanceEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexMaintenanceEditController]);

    function OpexMaintenanceEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'opexmaintenance-edit-view';
        vm.viewName = 'Opex Maintenance(Product Mapping)';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexMaintenance = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var opexMaintenanceRules = [];

        var setupRules = function () {

            //opexglbasis2Rules.push(new validator.PropertyRule("ServiceCode", {
            //    required: { message: "Service Code is required" }
            //}));
            //opexglbasis2Rules.push(new validator.PropertyRule("ServiceCategory", {
            //    required: { message: "ServiceCategory is required" }
            //}));
        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                // intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/opexmaintenance/getopexmaintenance/' + $stateParams.ID, null,
                        function (result) {
                            vm.opexMaintenance = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.opexMaintenance = {
                        CAPTION: '', PRODUCT: '', Active: true
                    };
            }
        };

        var initialView = function () {

        };

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.opexMaintenance, opexMaintenanceRules);
            vm.viewModelHelper.modelIsValid = vm.opexMaintenance.isValid;
            vm.viewModelHelper.modelErrors = vm.opexMaintenance.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/opexmaintenance/updateopexmaintenance', vm.opexMaintenance,
                    function (result) {

                        $state.go('mpr-opexmaintenance-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.opexMaintenance.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
                toastr.error(errorList, 'Fintrak');
            }

        };
        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/opexmaintenance/deleteopexmaintenance', vm.opexMaintenance.ID,//vm.activityBase.activitybaseId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-opexmaintenance-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-opexmaintenance-list');
        };

        setupRules();
        initialize();
    }
}());
