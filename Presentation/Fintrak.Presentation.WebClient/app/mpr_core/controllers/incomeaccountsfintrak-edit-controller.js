/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAccountsFintrakEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeAccountsFintrakEditController]);

    function IncomeAccountsFintrakEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomeaccountsfintrak-edit-view';
        vm.viewName = 'Income Accounts Fintrak';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeAccountsFintrak = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomeAccountsFintrakRules = [];

        var setupRules = function () {

            incomeAccountsFintrakRules.push(new validator.PropertyRule("AccountNumber", {
                required: { message: "AccountNumber is required" }
            }));

            incomeAccountsFintrakRules.push(new validator.PropertyRule("AccountOfficer_Code", {
                required: { message: "AccountOfficer_Code is required" }
            }));

            incomeAccountsFintrakRules.push(new validator.PropertyRule("CustomerName", {
                required: { message: "CustomerName is required" }
            }));

            incomeAccountsFintrakRules.push(new validator.PropertyRule("MIS_Code", {
                required: { message: "MisCode is required" }
            }));

            incomeAccountsFintrakRules.push(new validator.PropertyRule("OldMIS_Code", {
                required: { message: "OldMisCode is required" }
            }));

            incomeAccountsFintrakRules.push(new validator.PropertyRule("OldAccountOfficer_Code", {
                required: { message: "OldAccountOfficer_Code is required" }
            }));

            incomeAccountsFintrakRules.push(new validator.PropertyRule("ExpirationPeriod", {
                required: { message: "ExpirationPeriod is required" }
            }));

            incomeAccountsFintrakRules.push(new validator.PropertyRule("Comments", {
                required: { message: "Comments is required" }
            }));

            incomeAccountsFintrakRules.push(new validator.PropertyRule("ExpirationYear", {
                required: { message: "ExpirationYear is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomeaccountsfintrak/getincomeaccountsfintrak/' + $stateParams.ID, null,
                   function (result) {
                       vm.incomeAccountsFintrak = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomeAccountsFintrak = {
                        AccountNumber: '', CustomerName: '', MIS_Code: '', AccountOfficer_Code: '',
                        OldMIS_Code: '', OldAccountOfficerCode: '', ExpirationPeriod: '', Comments: '', ExpirationYear: '', Active: true
                    };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeAccountsFintrak, incomeAccountsFintrakRules);
            vm.viewModelHelper.modelIsValid = vm.incomeAccountsFintrak.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeAccountsFintrak.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomeaccountsfintrak/updateincomeaccountsfintrak', vm.incomeAccountsFintrak,
               function (result) {

                   $state.go('mpr-incomeaccountsfintrak-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeAccountsFintrak.errors;

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
                vm.viewModelHelper.apiPost('api/incomeaccountsfintrak/deleteincomeaccountsfintrak', vm.incomeAccountsFintrak.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomeaccountsfintrak-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomeaccountsfintrak-list');
        };

        setupRules();
        initialize();
    }
}());
