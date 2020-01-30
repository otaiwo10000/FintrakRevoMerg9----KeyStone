/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexGLBasis2EditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexGLBasis2EditController]);

    function OpexGLBasis2EditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'opexglbasis2-edit-view';
        vm.viewName = 'Opex GL Basis';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexglbasis2 = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var opexglbasis2Rules = [];

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
                    vm.viewModelHelper.apiGet('api/opexglbasis2/getopexglbasis2/' + $stateParams.ID, null,
                        function (result) {
                            vm.opexglbasis2 = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.opexglbasis2 = { CAPTION: '', MIS_CODE: '', BASIS: 0, NARRATIVE: '', Active: true };
            }
        };

        var initialView = function () {

        };

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.opexglbasis2, opexglbasis2Rules);
            vm.viewModelHelper.modelIsValid = vm.opexglbasis2.isValid;
            vm.viewModelHelper.modelErrors = vm.opexglbasis2.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/opexglbasis2/updateopexglbasis2', vm.opexglbasis2,
                    function (result) {

                        $state.go('mpr-opexglbasis2-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.opexglbasis2.errors;

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
                vm.viewModelHelper.apiPost('api/opexglbasis2/deleteopexglbasis2', vm.opexglbasis2.ID,//vm.activityBase.activitybaseId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-opexglbasis2-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-opexglbasis2-list');
        };

        setupRules();
        initialize();
    }
}());
