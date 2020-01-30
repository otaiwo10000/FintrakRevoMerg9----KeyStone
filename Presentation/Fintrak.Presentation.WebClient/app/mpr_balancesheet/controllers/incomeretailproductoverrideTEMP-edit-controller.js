/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeRetailProductOverrideTEMPEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$location',
            IncomeRetailProductOverrideTEMPEditController]);

    function IncomeRetailProductOverrideTEMPEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomeretailproductoverrideTEMP-edit-view';
        vm.viewName = 'Income Retail Product Override';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.bankList = [{ value: 'ACCESS', name: 'Access' }, { value: 'Diamond', name: 'Diamond' }];

        vm.icsprb = {};


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var icsprbRules = [];

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
                    vm.viewModelHelper.apiGet('api/incomeretailproductoverrideTEMP/getincomeretailproductoverride/' + $stateParams.Id, null,
                        function (result) {

                            vm.icsprb = result.data;

                            initialView();
                            vm.init === true;
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.icsprb = {
                        Customerid: 0, Bank: '', Mis_code: '', AccountOfficer_Code: '', ApprovalStatus: 'AWAITINGAPPROVAL', Active: true
                    };
            }
        };

        var initialView = function () {

        };

        vm.save = function () {

           // vm.icsprb.ApprovalStatus = 'AWAITINGAPPROVAL';
            ////Validate
            validator.ValidateModel(vm.icsprb, icsprbRules);
            vm.viewModelHelper.modelIsValid = vm.icsprb.isValid;
            vm.viewModelHelper.modelErrors = vm.icsprb.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomeretailproductoverrideTEMP/updateincomeretailproductoverride', vm.icsprb,
                    function (result) {

                        $state.go('mpr-incomeretailproductoverrideTEMP-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.icsprb.errors;

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
                vm.viewModelHelper.apiPost('api/incomeretailproductoverrideTEMP/deleteincomeretailproductoverride', vm.icsprb.Id,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-incomeretailproductoverrideTEMP-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-incomeretailproductoverrideTEMP-list');
        };

        

        //setupRules();
        initialize();


    }
}());
