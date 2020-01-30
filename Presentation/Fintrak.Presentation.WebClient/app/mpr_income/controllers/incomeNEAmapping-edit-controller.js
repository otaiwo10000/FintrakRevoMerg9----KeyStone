/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeNEAMappingEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$location',
            IncomeNEAMappingEditController]);

    function IncomeNEAMappingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomeNEAmapping-edit-view';
        vm.viewName = 'MPR Income NEA Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.ts = {};
        vm.ipt = {};
        vm.misproductcategoryList = [];
        vm.captionsList = [];
        //vm.plcaption2List = [];
        //vm.pprcaptionList = [];

        //vm.currencyList = [{ Value: 'LCY', Name: 'LCY' }, { Value: 'FCY', Name: 'FCY' }]
        //vm.categoryList = [{ Value: 'ASSET', Name: 'ASSET' }, { Value: 'LIABILITY', Name: 'LIABILITY' }]

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

                misproductcategoryFunc();
                captionsFunc();
                
                if ($stateParams.incomeNEAmappingId !== 0) {

                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomeNEAmapping/getincomeNEAmapping/' + $stateParams.incomeNEAmappingId, null,
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
                        Category_Code: '', Category_Description: '', Product_Code: '', Class: '', AssetType: '', Caption: '',
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

               vm.viewModelHelper.apiPost('api/incomeNEAmapping/updateincomeNEAmapping', vm.ipt,
               function (result) {

                   $state.go('mpr-incomeNEAmapping-list');
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
                vm.viewModelHelper.apiPost('api/incomeNEAmapping/deleteincomeNEAmapping', vm.ipt.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomeNEAmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomeNEAmapping-list');
        }

        var misproductcategoryFunc = function () {
            vm.viewModelHelper.apiGet('api/misproductcategoryinfo/getallmisproductcategoryinfo', null,
                function (result) {
                    vm.misproductcategoryList = result.data;
                },
                function (result) {
                    toastr.error('Fail to load users.', 'Fintrak');
                }, null);
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


        setupRules();
        initialize();


    }
}());
