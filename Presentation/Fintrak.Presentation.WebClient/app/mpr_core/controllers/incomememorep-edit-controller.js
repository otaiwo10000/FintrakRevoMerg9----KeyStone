/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeMemorepEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeMemorepEditController]);

    function IncomeMemorepEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomememorep-edit-view';
        vm.viewName = 'Income Memo';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeMemo = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomeMemorepRules = [];

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
                    vm.viewModelHelper.apiGet('api/incomememorep/getincomememorep/' + $stateParams.ID, null,
                   function (result) {
                       vm.incomeMemorep = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomeMemorep = { PL_CATEG: '', CATEGORYNAME: '', GLName: '', YEAR: '', Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeMemorep, incomeMemorepRules);
            vm.viewModelHelper.modelIsValid = vm.incomeMemorep.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeMemorep.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomememorep/updateincomememorep', vm.incomeMemorep,
               function (result) {

                   $state.go('mpr-incomememorep-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeMemorep.errors;

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
                vm.viewModelHelper.apiPost('api/incomememorep/deleteincomememorep', vm.incomeMemorep.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomememorep-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomememorep-list');
        };

        setupRules();
        initialize();
    }
}());
