/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexSBUBaseCostEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexSBUBaseCostEditController]);

    function OpexSBUBaseCostEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'opexsbubasecost-edit-view';
        vm.viewName = 'Opex Process(SBU BaseCost)';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexSBUBaseCost = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var opexSBUBaseCostRules = [];

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
                    vm.viewModelHelper.apiGet('api/opexsbubasecost/getopexsbubasecost/' + $stateParams.ID, null,
                        function (result) {
                            vm.opexSBUBaseCost = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.opexSBUBaseCost = {
                        MIS_CODE: '', AMOUNT: '', TEMPLATE: '', SOURCE: '', SN: '',
                        TRANS_TYPE: '', Active: true
                    };
            }
        };

        var initialView = function () {

        };

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.opexSBUBaseCost, opexSBUBaseCostRules);
            vm.viewModelHelper.modelIsValid = vm.opexSBUBaseCost.isValid;
            vm.viewModelHelper.modelErrors = vm.opexSBUBaseCost.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/opexsbubasecost/updateopexsbubasecost', vm.opexSBUBaseCost,
                    function (result) {

                        $state.go('mpr-opexsbubasecost-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.opexSBUBaseCost.errors;

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
                vm.viewModelHelper.apiPost('api/opexsbubasecost/deleteopexsbubasecost', vm.opexSBUBaseCost.ID,//vm.activityBase.activitybaseId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-opexsbubasecost-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-opexsbubasecost-list');
        };

        setupRules();
        initialize();
    }
}());
