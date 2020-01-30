/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeMonthsEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeMonthsEditController]);

    function IncomeMonthsEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'incomemonths-edit-view';
        vm.viewName = 'Financial Calender  - Update';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.im = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var imRules = [];

        var setupRules = function () {

            //staffRules.push(new validator.PropertyRule("StaffCode", {
            //    required: { message: "StaffCode is required" }
            //}));

            //staffRules.push(new validator.PropertyRule("Name", {
            //    required: { message: "Name is required" }
            //}));

            //staffRules.push(new validator.PropertyRule("Email", {
            //    required: { message: "Email is required" }
            //}));

        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomemonths/getincomemonths/' + $stateParams.ID, null,
                        function (result) {
                            vm.im = result.data;

                            initialView();
                            vm.init = true;
                            //
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.im = { Period: 0, Month: '', NumberOfDaysInMonth: 0, Year: 0, LastDay: 0, Active: true };
            }
        };


        var intializeLookUp = function () {
            //getCompanies();
            //getSolutions();
        };

        var initialView = function () {

        };


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.im, imRules);
            vm.viewModelHelper.modelIsValid = vm.im.isValid;
            vm.viewModelHelper.modelErrors = vm.im.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomemonths/updateincomemonths', vm.im,
                    function (result) {

                        $state.go('core-incomemonths-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.im.errors;

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
                vm.viewModelHelper.apiPost('api/incomemonths/deleteincomemonths', vm.im.ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('core-incomemonths-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('core-incomemonths-list');
        };

        setupRules();
        initialize(); 
    }
}());
