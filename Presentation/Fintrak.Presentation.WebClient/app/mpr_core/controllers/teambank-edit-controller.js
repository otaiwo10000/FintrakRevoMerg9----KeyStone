/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamBankEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        TeamBankEditController]);

    function TeamBankEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'teambank-edit-view';
        vm.viewName = 'MPR Team Bank';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.ts = {};
        vm.teambank = {};

        //vm.value1 = 'Checked'
        //vm.value2 = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var tsRules = [];

        var setupRules = function () {

            tsRules.push(new validator.PropertyRule("Accountofficer_Code", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("Team_Code", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("Branch_Code", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("Group_Code", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("Region_Code", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("Division_Code", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("DIRECTORATECODE", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("PPRCategory", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("Year", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("staff_id", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("unit_code", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("Period", {
                required: { message: "Account Officer Code is required" }
            }));

            //tsRules.push(new validator.PropertyRule("Numerator", {
            //    required: { message: "Numerator is required" }
            //}));

            //tsRules.push(new validator.PropertyRule("Denominator", {
            //    required: { message: "Denominator is required" }
            //}));

        }

        //var teamS = function () {
        //    vm.viewModelHelper.apiGet('api/teamstructure/GetAllData', null,
        //         function (result) {
        //             vm.teamstructure = result.data;
        //         },
        //         function (result) {
        //             toastr.error(result.data, 'Fintrak');
        //         }, null);
        //}

        //var mprDenominator = function () {
        //    vm.viewModelHelper.apiGet('api/ratiocaptionmapping/availableratioCaptionMappings', null,
        //         function (result) {
        //             vm.mprDenominators = result.data;
        //         },
        //         function (result) {
        //             toastr.error(result.data, 'Fintrak');
        //         }, null);
        //}



        var initialize = function () {
            if (vm.init === false) {

                if ($stateParams.teambankid !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/teambank/getteambank/' + $stateParams.teambankid, null,
                   function (result) {

                       vm.teambank = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    //vm.ratios = { MainCaption: '', Numerator: '', Denominator: '', ProRatio: '', Bsin: '', Active: true };
                    vm.teambank = {
                        AccountOfficer_Code: '', AccountOfficer_Name: '', Branch_Code: '', BranchName: '',
                        Division_Code: '', DivisionName: '', Region_Code: '', RegionName: '',                       
                        Team_Code: '', TeamName: '', Year: '', staff_id: '', Active: true                                                
                    };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {
            ////Validate
            validator.ValidateModel(vm.ts, tsRules);
            vm.viewModelHelper.modelIsValid = vm.ts.isValid;
            vm.viewModelHelper.modelErrors = vm.ts.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/teambank/updateteambank', vm.teambank,
               function (result) {

                   $state.go('mpr-teambank-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
          }
           else {
               vm.viewModelHelper.modelErrors = vm.ts.errors;

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
                vm.viewModelHelper.apiPost('api/teambank/deleteteambank', vm.teambank.TeamBankId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-ts-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-teambank-list');
        }


        //mprNumerator();
        //mprDenominator();
        setupRules();
        initialize();


    }
}());
