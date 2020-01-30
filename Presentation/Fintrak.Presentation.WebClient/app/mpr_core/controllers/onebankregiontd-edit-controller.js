/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OneBankRegionTDEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OneBankRegionTDEditController]);

    function OneBankRegionTDEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'onebankregiontd-edit-view';
        vm.viewName = 'OneBank Region TD';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.oneBankRegionTD = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var oneBankRegionTDRules = [];

        var setupRules = function () {

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/onebankregiontd/getonebankregiontd/' + $stateParams.ID, null,
                   function (result) {
                       vm.oneBankRegionTD = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.oneBankRegionTD = { StaffName: '', Grade_Levvel: '', Region_Code: '', CASA_Target: 0, Period: 0, Year: 0, Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.oneBankRegionTD, oneBankRegionTDRules);
            vm.viewModelHelper.modelIsValid = vm.oneBankRegionTD.isValid;
            vm.viewModelHelper.modelErrors = vm.oneBankRegionTD.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/onebankregiontd/updateonebankregiontd', vm.oneBankRegionTD,
               function (result) {

                   $state.go('mpr-onebankregiontd-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.oneBankRegionTD.errors;

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
                vm.viewModelHelper.apiPost('api/onebankregiontd/deleteonebankregiontd', vm.oneBankRegionTD.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-onebankregiontd-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-onebankregiontd-list');
        };

        setupRules();
        initialize();
    }
}());
