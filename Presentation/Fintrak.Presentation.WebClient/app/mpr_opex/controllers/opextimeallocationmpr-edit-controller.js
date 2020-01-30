/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexTimeAllocationMPREditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexTimeAllocationMPREditController]);

    function OpexTimeAllocationMPREditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'opextimeallocationmpr-edit-view';
        vm.viewName = 'Opex Time Allocation';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexTimeAllocationMPR = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var opexTimeAllocationMPRRules = [];

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
                    vm.viewModelHelper.apiGet('api/opextimeallocationmpr/getopextimeallocationmpr/' + $stateParams.ID, null,
                        function (result) {
                            vm.opexTimeAllocationMPR = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.opexTimeAllocationMPR = {
                        SOURCE: '', BASISCAPTION: '', TARGET: '', DESCRIPTION: '', RATIO: '',
                        TEMPLATE: '', SN: '', TOTAL: '', type: '', Active: true
                    };
            }
        };

        var initialView = function () {

        };

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.opexTimeAllocationMPR, opexTimeAllocationMPRRules);
            vm.viewModelHelper.modelIsValid = vm.opexTimeAllocationMPR.isValid;
            vm.viewModelHelper.modelErrors = vm.opexTimeAllocationMPR.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/opextimeallocationmpr/updateopextimeallocationmpr', vm.opexTimeAllocationMPR,
                    function (result) {

                        $state.go('mpr-opextimeallocationmpr-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.opexTimeAllocationMPR.errors;

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
                vm.viewModelHelper.apiPost('api/opextimeallocationmpr/deleteopextimeallocationmpr', vm.opexTimeAllocationMPR.ID,//vm.activityBase.activitybaseId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-opextimeallocationmpr-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-opextimeallocationmpr-list');
        };

        setupRules();
        initialize();
    }
}());
