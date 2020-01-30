/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeProductsTableUnitEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$location',
            IncomeProductsTableUnitEditController]);

    function IncomeProductsTableUnitEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomeproductstableunit-edit-view';
        vm.viewName = 'MPR Income Product Unit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.ts = {};
        vm.iptObj = {};
       

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
               
                if ($stateParams.iptUnitId !== 0) {

                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomeproductstableunit/getincomeproductunit/' + $stateParams.iptUnitId, null,
                   function (result) {

                       vm.iptObj = result.data;
                       
                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.iptObj = {
                        Product: '', ProductName: '', Unit: '', Active: true                                                                                          
                    };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {           

            ////Validate
            validator.ValidateModel(vm.iptObj, iptRules);
            vm.viewModelHelper.modelIsValid = vm.iptObj.isValid;
            vm.viewModelHelper.modelErrors = vm.iptObj.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/incomeproductstableunit/updateincomeproductunit', vm.iptObj,
               function (result) {

                   $state.go('mpr-incomeproductstableunit-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
          }
           else {
               vm.viewModelHelper.modelErrors = vm.iptObj.errors;

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
                vm.viewModelHelper.apiPost('api/incomeproductstableunit/deleteincomeproductunit', vm.iptObj.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomeproductstableunit-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomeproductstableunit-list');
        }

        


        setupRules();
        initialize();


    }
}());
