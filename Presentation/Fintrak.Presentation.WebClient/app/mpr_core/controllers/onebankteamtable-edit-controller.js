/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OneBankTeamTableEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OneBankTeamTableEditController]);

    function OneBankTeamTableEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'onebankteamtable-edit-view';
        vm.viewName = 'OneBank Team Table';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.oneBankTeamTable = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var oneBankTeamTableRules = [];

        var setupRules = function () {

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/onebankteamtable/getonebankteamtable/' + $stateParams.ID, null,
                   function (result) {
                       vm.oneBankTeamTable = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.oneBankTeamTable = { StaffName: '', Team_Code: '', GradeLevel: '', CASATarget: 0, Period: 0, Year: 0, Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.oneBankTeamTable, oneBankTeamTableRules);
            vm.viewModelHelper.modelIsValid = vm.oneBankTeamTable.isValid;
            vm.viewModelHelper.modelErrors = vm.oneBankTeamTable.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/onebankteamtable/updateonebankteamtable', vm.oneBankTeamTable,
               function (result) {

                   $state.go('mpr-onebankteamtable-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.oneBankTeamTable.errors;

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
                vm.viewModelHelper.apiPost('api/onebankteamtable/deleteonebankteamtable', vm.oneBankTeamTable.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-onebankteamtable-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-onebankteamtable-list');
        };

        setupRules();
        initialize();
    }
}());
