/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexUpdateEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexUpdateEditController]);

    function OpexUpdateEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'opexupdate-edit-view';
        vm.viewName = 'Opex Update';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexUpdate = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var opexUpdateRules = [];

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
                    vm.viewModelHelper.apiGet('api/opexupdate/getopexupdate/' + $stateParams.ID, null,
                        function (result) {
                            vm.opexUpdate = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.opexUpdate = {
                        OLD: '', NEW: '', Active: true
                    };
            }
        };

        var initialView = function () {

        };

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.opexUpdate, opexUpdateRules);
            vm.viewModelHelper.modelIsValid = vm.opexUpdate.isValid;
            vm.viewModelHelper.modelErrors = vm.opexUpdate.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/opexupdate/updateopexupdate', vm.opexUpdate,
                    function (result) {

                        $state.go('mpr-opexupdate-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.opexUpdate.errors;

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
                vm.viewModelHelper.apiPost('api/opexupdate/deleteopexupdate', vm.opexUpdate.ID,//vm.activityBase.activitybaseId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-opexupdate-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-opexupdate-list');
        };

        setupRules();
        initialize();
    }
}());
