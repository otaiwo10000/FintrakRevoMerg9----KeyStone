/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CustomerTransferPriceEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CustomerTransferPriceEditController]);

    function CustomerTransferPriceEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'customertransferprice-edit-view';
        vm.viewName = 'Customer Transfer Price';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.custtp = {};
        vm.companycode = '';

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var custtppRules = [];
    
        //vm.currencyList = [];
        vm.SolutionId = '0';
        

        //vm.currencyList = [
        //    { CurrencyType: 1, CurrencyTypeName: 'LCY' },
        //    { CurrencyType: 2, CurrencyTypeName: 'FCY' }
        //];

        vm.bsCategoryList = [
            { Category: 2, BSCategoryName: 'Asset' },
            { Category: 3, BSCategoryName: 'Liability' }
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

                if ($stateParams.customertransferpriceId !== 0) {

                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/customertransferprice/getcustomertransferprice/' + $stateParams.customertransferpriceId, null,
                        function (result) {

                            vm.custtp = result.data;
                            vm.custtp.CompanyCode = vm.companycode;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else {

                    vm.custtp = {
                        CustNo: '', Category: '', Rate: '',
                        Year: '', Period: '', SolutionId: '', CompanyCode: vm.companycode, Active: true
                    };
                };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {
            ////Validate
            validator.ValidateModel(vm.custtp, custtppRules);
            vm.viewModelHelper.modelIsValid = vm.custtp.isValid;
            vm.viewModelHelper.modelErrors = vm.custtp.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/customertransferprice/updatecustomertransferprice', vm.custtp,
               function (result) {

                   $state.go('mpr-customertransferprice-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
          }
           else {
               vm.viewModelHelper.modelErrors = vm.custtp.errors;

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
                vm.viewModelHelper.apiPost('api/customertransferprice/deletecustomertransferprice', vm.custtp.customertransferpriceId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-customertransferprice-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
                //}  //end deleteFlag
            }
        }

        vm.cancel = function () {
            $state.go('mpr-customertransferprice-list');
        }


        var compCode = function () {
            vm.viewModelHelper.apiGet('api/customertransferprice/companycode', null,
                function (result) {
                    vm.companycode = result.data;
                },
                function (result) {
                    toastr.error('Fail to load users.', 'Fintrak');
                }, null);
        };


        setupRules();
        compCode();
        initialize();

    }
}());
