/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ProductTransferPriceEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$location',
            ProductTransferPriceEditController]);

    function ProductTransferPriceEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'producttransferprice-edit-view';
        vm.viewName = 'Product Transfer Price';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.ts = {};
        vm.ptp = {};
        vm.product = [];

        //vm.value1 = 'Checked'
        //vm.value2 = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ptpRules = [];
      
        

        vm.bsCategoryList = [
            { BalanceSheetCategory: 2, BSCategoryName: 'Asset' },
            { BalanceSheetCategory: 3, BSCategoryName: 'Liability' }
        ];

        var setupRules = function () {

            //scmRules.push(new validator.PropertyRule("Metric", {
            //    required: { message: "Metric Name is required" }
            //}));

            //scmRules.push(new validator.PropertyRule("Metric_Code", {
            //    required: { message: "Metric Code is required" }
            //}));

            //scmRules.push(new validator.PropertyRule("Period", {
            //    required: { message: "Period is required" }
            //}));

            //scmRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Year is required" }
            //}));          
        }

        var initialize = function () {

            getProducts();
            if (vm.init === false) {

                if ($stateParams.ID !== 0) {

                    vm.acctText = 'A';
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/producttransferprice/getproducttransferprice/' + $stateParams.ID, null,
                   function (result) {

                       vm.ptp = result.data;                      

                       initialView();
                       vm.init === true;
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.ptp = {
                        ProductCode: '', Rating: '', Description: '', Category: '', Active: true                                             
                    };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {           

            ////Validate
            validator.ValidateModel(vm.ptp, ptpRules);
            vm.viewModelHelper.modelIsValid = vm.ptp.isValid;
            vm.viewModelHelper.modelErrors = vm.ptp.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/producttransferprice/updateproducttransferprice', vm.ptp,
               function (result) {

                   $state.go('mpr-producttransferprice-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
          }
           else {
               vm.viewModelHelper.modelErrors = vm.ptp.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/producttransferprice/deleteproducttransferprice', vm.ptp.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-deleteproducttransferprice-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-deleteproducttransferprice-list');
        }

        var getProducts = function () {
            vm.viewModelHelper.apiGet('api/product/availableproducts', null,
                function (result) {
                    vm.products = result.data;
                },
                function (result) {

                }, null);
        }

       

        setupRules();
        initialize();


    }
}());
