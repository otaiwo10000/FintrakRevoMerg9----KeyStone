/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCommFeeBusinessRuleEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeCommFeeBusinessRuleEditController]);

    function IncomeCommFeeBusinessRuleEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomecommfeebusinessrule-edit-view';
        vm.viewName = 'Income CommFee Business Rule';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeCommFeeBusinessRule = {};

        vm.bankList = [{ value: 'ACCESS', name: 'Access' }, { value: 'Diamond', name: 'Diamond' }];
        vm.relatedList = [{ value: 'Yes', name: 'Yes' }, { value: 'No', name: 'No' }];
        vm.branchesList = [{ value: 'Yes', name: 'Yes' }, { value: 'No', name: 'No' }];
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomeCommFeeBusinessRuleRules = [];

        var setupRules = function () {

            //productmisRules.push(new validator.PropertyRule("ProductCode", {
            //    required: { message: "Product is required" }
            //}));

            //productmisRules.push(new validator.PropertyRule("CaptionCode", {
            //    required: { message: "Caption is required" }
            //}));

            //productmisRules.push(new validator.PropertyRule("TeamDefinitionCode", {
            //    required: { message: "Team Definition is required" }
            //}));

            //productmisRules.push(new validator.PropertyRule("TeamCode", {
            //    required: { message: "Team is required" }
            //}));
        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomecommfeebusinessrule/getincomecommfeebusinessrule/' + $stateParams.ID, null,
                        function (result) {
                            vm.incomeCommFeeBusinessRule = result.data;



                            //initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.incomeCommFeeBusinessRule = { GLCode: '', Bank: '', GL_Description: '', Channel: '', Related_Account: '', Branches: '', Basis_of_Allocation: '', rule: '', Active: true };
            }
        };

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeCommFeeBusinessRule, incomeCommFeeBusinessRuleRules);
            vm.viewModelHelper.modelIsValid = vm.incomeCommFeeBusinessRule.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeCommFeeBusinessRule.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomecommfeebusinessrule/updateincomecommfeebusinessrule', vm.incomeCommFeeBusinessRule,
                    function (result) {

                        $state.go('mpr-incomecommfeebusinessrule-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeCommFeeBusinessRule.errors;

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
                vm.viewModelHelper.apiPost('api/incomecommfeebusinessrule/deleteincomecommfeebusinessrule', vm.incomeCommFeeBusinessRule.ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-incomecommfeebusinessrule-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-incomecommfeebusinessrule-list');
        };


    

        setupRules();
        initialize(); 
    }
}());
