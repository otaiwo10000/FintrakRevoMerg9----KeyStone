/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GeneralEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        GeneralEditController]);

    function GeneralEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'general-edit-view';
        vm.viewName = 'General';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.general = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var generalRules = [];

        var setupRules = function () {
            generalRules.push(new validator.PropertyRule("Email", {
                required: { message: "Invalid email address" }
            }));
           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initialLookUp();

                vm.viewModelHelper.apiGet('api/general/getgeneral', null,
                  function (result) {
                      vm.general = result.data;

                      if (vm.general === 'null')
                          vm.general = { Host: '', Email: '', Password: '', Active: true };

                      initialView();
                      vm.init === true;
                      //
                  },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
            }
        }

        var initialLookUp = function () {
            
        }

        var initialView = function () {
  
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.general, generalRules);
            vm.viewModelHelper.modelIsValid = vm.general.isValid;
            vm.viewModelHelper.modelErrors = vm.general.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/general/updategeneral', vm.general,
               function (result) {
                   
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.general.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

        setupRules();
        initialize(); 
    }
}());
