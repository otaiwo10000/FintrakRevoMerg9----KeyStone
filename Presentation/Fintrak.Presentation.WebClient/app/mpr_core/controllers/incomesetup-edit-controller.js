/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeSetupEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeSetupEditController]);

    function IncomeSetupEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomesetup-edit-view';
        vm.viewName = 'INCOME SETUP';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeSetup = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomesetupRules = [];

        var setupRules = function () {

           // incomesetupRules.push(new validator.PropertyRule("Branch", {
           //     required: { message: "Branch is required" }
           // }));

           

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomesetup/getincomesetup/' + $stateParams.ID, null,
                        function (result) {
                            vm.incomeSetup = result.data;

                            //initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    //vm.incomeSetup = {
                    //    NDIC: '', CRR: '', Year: '', PDLProductCode: '', FinYearCount: '', GLLP: '', StartDate: '', EndDate: '',
                    //    TaxRate: '', Runmode: '', ReportMode: '', ExcoMIS: '', HRMIS: '', ManagingSharePerc: '', BrokerSharePerc: '',
                    //    SFU: '', ExcoAccountOfficer: '', ProPrietryMIS: '', Othermis: '', accountnumberdigit: '', localcurrcode: '',
                    //    LMP: '', CRRInt: '', LMPInt: '', Vault_Cash: '', Title: '', Active: true
                    //};

                    //vm.incomeSetup = {
                    //    CurrentPeriod: '', Year: '', StartDate: '', EndDate: '',
                    //    Runmode: '', ReportMode: '', Active: true
                    //};

                     vm.incomeSetup = {
                         NDIC: '', CRR: '', CurrentPeriod: '', Year: '', PDLProductCode: '', FinYearCount: '', GLLP: '', StartDate: '', EndDate: '',
                        TaxRate: '', Runmode: '', ReportMode: '', ExcoMIS: '', HRMIS: '', ManagingSharePerc: '', BrokerSharePerc: '',
                        SFU: '', Vault_Cash: '', Active: true
                    };
            }
        };


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeSetup, incomesetupRules);
            vm.viewModelHelper.modelIsValid = vm.incomeSetup.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeSetup.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomesetup/updateincomesetup', vm.incomeSetup,
               function (result) {

                   $state.go('mpr-incomesetup-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeSetup.errors;

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
                vm.viewModelHelper.apiPost('api/incomesetup/deleteincomesetup', vm.incomeSetup.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomesetup-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomesetup-list');
        };

        setupRules();
        initialize();
    }
}());
