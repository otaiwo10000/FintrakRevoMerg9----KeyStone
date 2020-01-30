/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("PlacementEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        PlacementEditController]);

    function PlacementEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS';
        vm.view = 'placement-edit-view';
        vm.viewName = 'Placements';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.placement = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var placementRules = [];


        var setupRules = function () {
          
            placementRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));

            placementRules.push(new validator.PropertyRule("CustomerName", {
                required: { message: "Customer Name is required" }
            }));

            placementRules.push(new validator.PropertyRule("Rating", {
                required: { message: "Rating is required" }
            }));

            placementRules.push(new validator.PropertyRule("Amount", {
                required: { message: "Amount is required" }
            }));

            placementRules.push(new validator.PropertyRule("LCY_Amount", {
                required: { message: "Local Currency Amount is required" }
            }));


        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.Placement_Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/placement/getplacement/' + $stateParams.Placement_Id, null,
                   function (result) {
                       vm.placement = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.placement = { RefNo: '', CustomerName: '', Rating: '', BookingDate: '', ValueDate: '', MaturityDate: '', Amount: 0, Rate: 0, Currency: '', ExchangeRate: 0, LCY_Amount: 0, CollateralType: '', CollateralValue: 0, CollateralHaircut: 0, RunDate: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            //getSectors()
            //getVariables()
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.placement, placementRules);
            vm.viewModelHelper.modelIsValid = vm.placement.isValid;
            vm.viewModelHelper.modelErrors = vm.placement.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/placement/updateplacement', vm.placement,
               function (result) {
                   
                   $state.go('ifrs9-placement-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.placement.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/placement/deleteplacement', vm.placement.Placement_Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-placement-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-placement-list');
        };


        setupRules();
        initialize(); 
    }
}());
