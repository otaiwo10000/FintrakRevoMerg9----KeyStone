/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanDetailEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        LoanDetailEditController]);

    function LoanDetailEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Loan Detail Data';
        vm.view = 'loandetail-edit-view';
        vm.viewName = 'Loan Details Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.loanDetail = {};
        vm.scheduleTypes = [];
        vm.classification = '';
        vm.subClassification = '';
              
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
       
        vm.classifications = [
            {id: 1, Name: 'PERFORMING'},
            { id: 2, Name: 'NON-PERFORMING' }
        ];

        vm.stages = [
           { id: 1, Name: 1 },           
           { id: 2, Name: 2 },
           { id: 3, Name: 3 },
        ];
       
        vm.subClassifications = [
            { id: 1, Name: 'STANDARD' },
            { id: 2, Name: 'SUB-STANDARD' },
            { id: 3, Name: 'WATCHLIST' },
            { id: 4, Name: 'DOUBTFUL' },
            { id: 5, Name: 'LOST' },
        ];


        var loandetailRules = [];

        var setupRules = function () {
          
            loandetailRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));
      
            loandetailRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "AccountNo is required" }
            }));

            loandetailRules.push(new validator.PropertyRule("ValueDate", {
                notZero: { message: "ValueDate is required" }
            }));

            loandetailRules.push(new validator.PropertyRule("MaturityDate", {
                notZero: { message: "MaturityDate is required" }
            }));
            
            loandetailRules.push(new validator.PropertyRule("Amount", {
                required: { message: "Amount is required" }
            }));

            loandetailRules.push(new validator.PropertyRule("Rate", {
                notZero: { message: "Rate is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.loanDetailId !== 0)
                {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/rawloandetail/getrawloandetail/' + $stateParams.loanDetailId, null,
                   function (result) {
                       vm.loanDetail = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.loanDetail = { RefNo: '', AccountNo: '', ValueDate: '', MaturityDate: '',  Rate: '', Active: true,CollateralValue:0,ProductCategory:'LOANS' };
            }
        }

        var intializeLookUp = function () {
            
            }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.loanDetail, loandetailRules);
            vm.viewModelHelper.modelIsValid = vm.loanDetail.isValid;
            vm.viewModelHelper.modelErrors = vm.loanDetail.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/rawloandetail/updaterawloandetail', vm.loanDetail,
               function (result) {
                   vm.updateloanclassificationotch();
                   $state.go('ifrs-loandetail-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.loanDetail.errors;

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
                vm.viewModelHelper.apiPost('api/rawloandetail/deleteloandetail', vm.loanDetail.LoanDetailId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-loandetail-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.updateloanclassificationotch = function () {
            var params = { RefNo:vm.loanDetail.RefNo ,Rating: vm.loanDetail.Sector, Stage: vm.loanDetail.Stage };
            vm.viewModelHelper.apiPost('api/rawloandetail/updateloanclassnotch', params,
                      function (result) {
                        //  $state.go('ifrs-loandetail-list');
                         // vm.getUpdatedMarketYieldData();
                      },
                     function (result) {
                         toastr.error(result.data, 'Fintrak Error');
                     }, null);
        }


        vm.cancel = function () {
            $state.go('ifrs-loandetail-list');
        };

        vm.openRunDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate = true;
        }
        vm.openRunDate2 = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate2 = true;
        }
        vm.openRunDate3 = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate3 = true;
        }

        setupRules();
        initialize(); 
    }
}());
