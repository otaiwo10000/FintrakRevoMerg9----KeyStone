/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexStaffcostDetailEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexStaffcostDetailEditController]);

    function OpexStaffcostDetailEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'opexstaffcostdetail-edit-view';
        vm.viewName = 'Opex Staff Cost';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexStaffcostDetail = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var opexStaffcostDetailRules = [];

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
                    vm.viewModelHelper.apiGet('api/opexstaffcostdetail/getopexstaffcostdetail/' + $stateParams.ID, null,
                        function (result) {
                            vm.opexStaffcostDetail = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.opexStaffcostDetail = {
                        EMP_NAME: '', AMOUNT: '', PERIOD: '', YEAR: '', TEAM_CODE: '',
                        EMP_ID: '', ACCOUNTOFFICER_CODE: '', Active: true
                    };
            }
        };

        var initialView = function () {

        };

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.opexStaffcostDetail, opexStaffcostDetailRules);
            vm.viewModelHelper.modelIsValid = vm.opexStaffcostDetail.isValid;
            vm.viewModelHelper.modelErrors = vm.opexStaffcostDetail.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/opexstaffcostdetail/updateopexstaffcostdetail', vm.opexStaffcostDetail,
                    function (result) {

                        $state.go('mpr-opexstaffcostdetail-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.opexStaffcostDetail.errors;

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
                vm.viewModelHelper.apiPost('api/opexstaffcostdetail/deleteopexstaffcostdetail', vm.opexStaffcostDetail.ID,//vm.activityBase.activitybaseId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-opexstaffcostdetail-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-opexstaffcostdetail-list');
        };

        setupRules();
        initialize();
    }
}());
