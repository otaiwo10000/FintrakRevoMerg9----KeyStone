/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FTPRiskRatingsEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        FTPRiskRatingsEditController]);

    function FTPRiskRatingsEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'ftpriskratings-edit-view';
        vm.viewName = 'FTP Risk Ratings';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.fTPRiskRatings = {};

        vm.currencyList = [{name: 'LCY', value: 'LCY'}, {name: 'FCY', value: 'FCY'}];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var fTPRiskRatingsRules = [];

        var setupRules = function () {

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ftpriskratings/getftpriskratings/' + $stateParams.ID, null,
                   function (result) {
                       vm.fTPRiskRatings = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.fTPRiskRatings = {
                        Ratings: '', Currency: '', Category: '', Caption: '', Levels: '', LevelCode: '', JAN: '', Feb: '',
                        Mar: '', Apr: '', May: '', Jun: '', Jul: '', Aug: '', Sep: '', Oct: '', Nov: '', Dec: '', Year: '', Active: true
                    };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.fTPRiskRatings, fTPRiskRatingsRules);
            vm.viewModelHelper.modelIsValid = vm.fTPRiskRatings.isValid;
            vm.viewModelHelper.modelErrors = vm.fTPRiskRatings.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/ftpriskratings/updateftpriskratings', vm.fTPRiskRatings,
               function (result) {

                   $state.go('mpr-ftpriskratings-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.fTPRiskRatings.errors;

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
                vm.viewModelHelper.apiPost('api/ftpriskratings/deleteftpriskratings', vm.fTPRiskRatings.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-ftpriskratings-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-ftpriskratings-list');
        };

        setupRules();
        initialize();
    }
}());
