/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SlaryScheduleEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SlaryScheduleEditController]);

    function SlaryScheduleEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'slaryschedule-edit-view';
        vm.viewName = 'Slary Schedule';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.slaryschedule = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var slaryScheduleRules = [];

        var setupRules = function () {

            //slaryScheduleRules.push(new validator.PropertyRule("Branch", {
            //    required: { message: "Branch is required" }
            //}));

            //slaryScheduleRules.push(new validator.PropertyRule("Percentage", {
            //    required: { message: "Percentage is required" }
            //}));

        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/slaryschedule/availableslaryschedule/' + $stateParams.ID, null,
                        function (result) {
                            vm.slaryschedule = result.data;

                            //initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.slaryschedule = { EmpID: '', EMP_Name: '', Emp_Level: '', MIS_Code: '', Amount: 0, Pay_Code: '', Location: '', SBU: '', Sol: '', AnnualPay: 0, SType: '', Active: true };
            }
        };


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.slaryschedule, slaryScheduleRules);
            vm.viewModelHelper.modelIsValid = vm.slaryschedule.isValid;
            vm.viewModelHelper.modelErrors = vm.slaryschedule.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/slaryschedule/updateslaryschedule', vm.slaryschedule,
                    function (result) {

                        $state.go('mpr-slaryschedule-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.abcRatio.errors;

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
                vm.viewModelHelper.apiPost('api/slaryschedule/deleteslaryschedule', vm.abcRatio.AbcRatioId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-slaryschedule-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-slaryschedule-list');
        };

        setupRules();
        initialize();
    }
}());
