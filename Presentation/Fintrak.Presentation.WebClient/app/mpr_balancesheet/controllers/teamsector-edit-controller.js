/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamSectorEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        TeamSectorEditController]);

    function TeamSectorEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'teamsector-edit-view';
        vm.viewName = 'Team Sector';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.teamsector = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var teamsectorRules = [];
    
       
        var setupRules = function () {   

            //ctppRules.push(new validator.PropertyRule("DefinitionCode", {
            //    required: { message: "Definition Code is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("MisCode", {
            //    required: { message: "MisCode is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("Currency", {
            //    required: { message: "Currency is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("Rate", {
            //    required: { message: "Rate is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("Period", {
            //    required: { message: "Period is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Year is required" }
            //}));          
        }

        var initialize = function () {
            if (vm.init === false) {

                if ($stateParams.Mpr_Team_Sector_ID !== 0) {
                    
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/teamsector/getteamsector/' + $stateParams.Mpr_Team_Sector_ID, null,
                   function (result) {

                       vm.teamsector = result.data;
                      
                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    
                    vm.teamsector = {
                        Level1Code: '', Level1Name: '', Level2Code: '', Level2Name: '',
                        Level3Code: '', Level3Name: '', Level4Code: '', Level4Name: '', Active: true                                              
                    };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {
            ////Validate
            validator.ValidateModel(vm.teamsector, teamsectorRules);
            vm.viewModelHelper.modelIsValid = vm.teamsector.isValid;
            vm.viewModelHelper.modelErrors = vm.teamsector.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/teamsector/updateteamsector', vm.teamsector,
               function (result) {

                   $state.go('mpr-teamsector-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
          }
           else {
               vm.viewModelHelper.modelErrors = vm.teamsector.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.delete = function () {
            //var deleteFlag = $window.confirm(' Are you sure you want to delete');
            //if (confirm("Click OK to delete item ID " + appid + "  " + "whose name is:" + "  " + fname + "  " + lname + "?"))     //this is javascript function.
            if (confirm('Are you sure you want to delete the selected item?'))     //this is javascript function.
            {
                //if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/teamsector/deleteteamsector', vm.teamsector.Mpr_Team_Sector_ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-teamsector-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
                //}  //end deleteFlag
            }
        }

        vm.cancel = function () {
            $state.go('mpr-teamsector-list');
        }


        setupRules();
        initialize();

    }
}());
