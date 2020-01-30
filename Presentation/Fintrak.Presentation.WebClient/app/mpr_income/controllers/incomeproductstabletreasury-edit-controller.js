/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeProductsTableTreasuryEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$location',
            IncomeProductsTableTreasuryEditController]);

    function IncomeProductsTableTreasuryEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomeproductstabletreasury-edit-view';
        vm.viewName = 'MPR IncomeProductsTable Treasury';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.ts = {};
        vm.ipt = {};
        vm.captionsList = [];
        vm.plcaption2List = [];
        vm.pprcaptionList = [];

        vm.currencyList = [{ Value: 'LCY', Name: 'LCY' }, { Value: 'FCY', Name: 'FCY' }]
        vm.categoryList = [{ Value: 'ASSET', Name: 'ASSET' }, { Value: 'LIABILITY', Name: 'LIABILITY' }]

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var iptRules = [];
       
        var setupRules = function () {

            iptRules.push(new validator.PropertyRule("Metric", {
                required: { message: "Metric Name is required" }
            }));

            iptRules.push(new validator.PropertyRule("Metric_Code", {
                required: { message: "Metric Code is required" }
            }));

            iptRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));

            iptRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));          
        }

        var initialize = function () {
            if (vm.init === false) {

                captionsFunc();
                
                if ($stateParams.iptTreasuryId !== 0) {

                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomeproductstabletreasury/getincomeproducttabletreasury/' + $stateParams.iptTreasuryId, null,
                   function (result) {

                       vm.ipt = result.data;
                       
                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.ipt = {
                        GLCode: '', Caption: '', Type: '', Status: '',
                        Currency: '', SBUCode: '', Category: '',
                        Active: true                                              
                    };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {           

            ////Validate
            validator.ValidateModel(vm.ipt, iptRules);
            vm.viewModelHelper.modelIsValid = vm.ipt.isValid;
            vm.viewModelHelper.modelErrors = vm.ipt.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/incomeproductstabletreasury/updateincomeproducttabletreasury', vm.ipt,
               function (result) {

                   $state.go('mpr-incomeproductstabletreasury-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
          }
           else {
               vm.viewModelHelper.modelErrors = vm.scm.errors;

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
                vm.viewModelHelper.apiPost('api/incomeproductstabletreasury/deleteincomeproducttabletreasury', vm.ipt.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomeproductstabletreasury-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomeproductstabletreasury-list');
        }

        var captionsFunc = function () {
            vm.viewModelHelper.apiGet('api/caption/getallcaptions', null,
                function (result) {
                    vm.captionsList = result.data;
                },
                function (result) {
                    toastr.error('Fail to load users.', 'Fintrak');
                }, null);
        }

        //app.service('Products', function () {
        //    this.Items = function () {
        //        // if we want can get data from database 
        //        product = { product: '', price: '' }
        //    };
        //    return this;
        //});


        setupRules();
        initialize();


    }
}());
