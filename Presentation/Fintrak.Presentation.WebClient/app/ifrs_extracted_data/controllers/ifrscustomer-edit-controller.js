/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsCustomerEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IfrsCustomerEditController]);

    function IfrsCustomerEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IfrsCustomer';
        vm.view = 'ifrscustomer-edit-view';
        vm.viewName = 'IFRS Customer';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifrsCustomers = {};
        vm.creditRiskCode = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
 
        var ifrsCustomerRules = [];

        //Applicable to Coronation
        vm.lgdTypes = [
      { Id: 1, Name: 'Strong' },
      { Id: 2, Name: 'Adequate' },
      { Id: 3, Name: 'Weak' },
      { Id: 4, Name: 'Unsecured' },
      { Id: 5, Name: 'Subordinated' }
        ];

        var setupRules = function () {
          
            ifrsCustomerRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));

            ifrsCustomerRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "AccountNo is required" }
            }));

            ifrsCustomerRules.push(new validator.PropertyRule("Date", {
                notZero: { message: "Date is required" }
            }));

            ifrsCustomerRules.push(new validator.PropertyRule("FeeAmount", {
                required: { message: "FeeAmount is required" }
            }));

            ifrsCustomerRules.push(new validator.PropertyRule("Description", {
                required: { message: "Description is required" }
            }));

        }


        var intializeLookUp = function () {
            //getProducts();
            // getLoanProducts();
            getRiskRatings();
        }
        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ifrsCustomerId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ifrscustomer/getcustomer/' + $stateParams.ifrsCustomerId, null,
                   function (result) {
                       vm.ifrsCustomers = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.ifrsCustomers = { CustomerNo: '', CustomerName: '', CreditRating: '', LGD_Type: '', CustType: '', CompanyCode: '', Active: true };
            }
        }


        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ifrsCustomers, ifrsCustomerRules);
            vm.viewModelHelper.modelIsValid = vm.ifrsCustomers.isValid;
            vm.viewModelHelper.modelErrors = vm.ifrsCustomers.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/ifrscustomer/updateifrscustomer', vm.ifrsCustomers,
               function (result) {
                   
                   $state.go('ifrs-ifrscustomer-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.ifrsCustomer.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/ifrscustomer/deleteifrscustomer', vm.ifrsCustomer.IfrsCustomerId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-ifrscustomer-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-ifrscustomer-list');
        };

       
        var getRiskRatings = function () {
            vm.viewModelHelper.apiGet('api/creditriskrating/getRiskRatingCode', null,
                 function (result) {
                     vm.creditRiskCode = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }
        setupRules();
        initialize(); 
    }
}());
