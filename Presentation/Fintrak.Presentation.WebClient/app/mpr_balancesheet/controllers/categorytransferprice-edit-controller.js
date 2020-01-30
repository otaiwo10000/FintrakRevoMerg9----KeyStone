/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CategoryTransferPriceEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CategoryTransferPriceEditController]);

    function CategoryTransferPriceEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'categorytransferprice-edit-view';
        vm.viewName = 'Category Transfer Price';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ctp = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ctppRules = [];
    
        //vm.currencyList = [];
        vm.SolutionId = '0';
        vm.CompanyCode = 'STB';

        vm.currencyList = [
            { CurrencyType: 1, CurrencyTypeName: 'LCY' },
            { CurrencyType: 2, CurrencyTypeName: 'FCY' }
        ];

        vm.bsCategoryList = [
            { BalanceSheetCategory: 2, BSCategoryName: 'Asset' },
            { BalanceSheetCategory: 3, BSCategoryName: 'Liability' }
        ];


        var setupRules = function () {   

            //ctppRules.push(new validator.PropertyRule("DefinitionCode", {
            //    required: { message: "Definition Code is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("MisCode", {
            //    required: { message: "MisCode is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("Currency", {
            //    required: { message: "Currency is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("Rate", {
            //    required: { message: "Rate is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("Period", {
            //    required: { message: "Period is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Year is required" }
            //}));          
        }

        var initialize = function () {
            if (vm.init === false) {

                if ($stateParams.CategoryTransferPriceId !== 0) {
                    
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/categorytransferprice/getcategorytransferprice/' + $stateParams.CategoryTransferPriceId, null,
                   function (result) {

                       vm.ctp = result.data;
                      
                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    
                    vm.ctp = {
                        BalanceSheetCategory: '', CurrencyType: '',
                        Rate: '', Year: '', Period: '', Active: true                                              
                    };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {
            ////Validate
            validator.ValidateModel(vm.ctp, ctppRules);
            vm.viewModelHelper.modelIsValid = vm.ctp.isValid;
            vm.viewModelHelper.modelErrors = vm.ctp.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/categorytransferprice/updatecategorytransferprice', vm.ctp,
               function (result) {

                   $state.go('mpr-categorytransferprice-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
          }
           else {
               vm.viewModelHelper.modelErrors = vm.ctp.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.delete = function () {
            //var deleteFlag = $window.confirm(' Are you sure you want to delete');
            //if (confirm("Click OK to delete item ID " + appid + "  " + "whose name is:" + "  " + fname + "  " + lname + "?"))     //this is javascript function.
            if (confirm('Are you sure you want to delete the selected item?'))     //this is javascript function.
            {
                //if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/categorytransferprice/deletecategorytransferprice', vm.ctp.CategoryTransferPriceId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-categorytransferprice-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
                //}  //end deleteFlag
            }
        }

        vm.cancel = function () {
            $state.go('mpr-categorytransferprice-list');
        }


        setupRules();
        initialize();

    }
}());
