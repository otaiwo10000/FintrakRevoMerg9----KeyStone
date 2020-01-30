/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeSplitPoolsRateEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeSplitPoolsRateEditController]);

    function IncomeSplitPoolsRateEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomesplitpoolsrate-edit-view';
        vm.viewName = 'Income Split PoolsRate';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeSplitPoolsRate = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomeSplitPoolsRateRules = [];

        var setupRules = function () {

            //incomeMemorepRules.push(new validator.PropertyRule("OldMIS_Code", {
            //    required: { message: "Old Mis Code is required" }
            //}));

            //incomeMemorepRules.push(new validator.PropertyRule("NewMIS_Code", {
            //    required: { message: "New Mis Code is required" }
            //}));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomesplitpoolsrate/getincomesplitpoolsrate/' + $stateParams.ID, null,
                   function (result) {
                       vm.incomeSplitPoolsRate = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomeSplitPoolsRate = { PoolRateLCYAsset: '', PoolRateLCYLiability: '', PoolRateFCYAsset: '', PoolRateFCYLiability: '', Period: '', Year: '', Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeSplitPoolsRate, incomeSplitPoolsRateRules);
            vm.viewModelHelper.modelIsValid = vm.incomeSplitPoolsRate.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeSplitPoolsRate.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomesplitpoolsrate/updateincomesplitpoolsrate', vm.incomeSplitPoolsRate,
               function (result) {

                   $state.go('mpr-incomesplitpoolsrate-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeSplitPoolsRate.errors;

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
                vm.viewModelHelper.apiPost('api/incomesplitpoolsrate/deleteincomesplitpoolsrate', vm.incomeSplitPoolsRate.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomesplitpoolsrate-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomesplitpoolsrate-list');
        };

        setupRules();
        initialize();
    }
}());
