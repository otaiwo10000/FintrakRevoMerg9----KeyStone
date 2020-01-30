/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCustomerRatingOverrideEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$location',
            IncomeCustomerRatingOverrideEditController]);

    function IncomeCustomerRatingOverrideEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomecustomerratingoverride-edit-view';
        vm.viewName = 'Income Customer Rating Override';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.icro = {};


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var icroRules = [];

        var setupRules = function () {

            //icsprbRules.push(new validator.PropertyRule("Metric", {
            //    required: { message: "Metric is required" }
            //}));

            //scmRules.push(new validator.PropertyRule("Period", {
            //    required: { message: "Period is required" }
            //}));

            //scmRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Year is required" }
            //}));          
        };

        var initialize = function () {

            if (vm.init === false) {

                if ($stateParams.Id !== 0) {

                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomecustomerratingoverride/getincomecustomerratingoverride/' + $stateParams.Id, null,
                        function (result) {

                            vm.icro = result.data;

                            initialView();
                            vm.init === true;
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.icro = {
                        Cust_ID: '', Ref_No: '', Settlement_Account: '', Customer_Name: '', Limit: 0,
                        PrincipalOutstandingBal: 0, Value_Date: '', Maturity_Date: '', Risk_Rating: '',
                        Active: true
                    };
            }
        };

        var initialView = function () {

        };

        vm.save = function () {

            ////Validate
            validator.ValidateModel(vm.icro, icroRules);
            vm.viewModelHelper.modelIsValid = vm.icro.isValid;
            vm.viewModelHelper.modelErrors = vm.icro.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomecustomerratingoverride/updateincomecustomerratingoverride', vm.icro,
                    function (result) {

                        $state.go('mpr-incomecustomerratingoverride-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.icro.errors;

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
                vm.viewModelHelper.apiPost('api/incomecustomerratingoverride/deleteincomecustomerratingoverride', vm.icro.Id,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-incomecustomerratingoverride-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-incomecustomerratingoverride-list');
        };

        

        //setupRules();
        initialize();


    }
}());
