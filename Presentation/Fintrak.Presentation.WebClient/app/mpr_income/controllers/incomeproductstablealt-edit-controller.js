/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeProductsTableALTEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$location',
                        IncomeProductsTableALTEditController]);

    function IncomeProductsTableALTEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomeproductstablealt-edit-view';
        vm.viewName = 'MPR Income Product';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.ts = {};
        vm.ipt = {};
        vm.captionsList = [];
        //vm.plcaption2List = [];
        vm.incomecaptionList = [];
        vm.pprcaptionList = [];
        vm.groupcaptionsList = [];
        vm.clist = false;
        vm.cedit = false;

        vm.currencyList = [{ Value: 'LCY', Name: 'LCY' }, { Value: 'FCY', Name: 'FCY' }]
        vm.categoryList = [{ Value: 'ASSET', Name: 'ASSET' }, { Value: 'LIABILITY', Name: 'LIABILITY' }]
        vm.categoryFilterList = [{ Value: 'ON', Name: 'ON' }, { Value: 'OFF', Name: 'OFF' }]
        vm.pprstatusList = [{ Value: ' ', Name: ' ' }, { Value: 'Y', Name: 'Y' }, { Value: 'N', Name: 'N' }]
        vm.aprstatusList = [{ Value: 'True', Name: 'True' }, { Value: 'False', Name: 'False' }]

        //vm.value1 = 'Checked'
        //vm.value2 = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var iptRules = [];
       
        var setupRules = function () {

            iptRules.push(new validator.PropertyRule("Metric", {
                required: { message: "Metric Name is required" }
            }));

            iptRules.push(new validator.PropertyRule("Metric_Code", {
                required: { message: "Metric Code is required" }
            }));

            iptRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));

            iptRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));          
        }

        var initialize = function () {
            if (vm.init === false) {

                captionsFunc();
                incomeCaptionFunc();
                pprCaptionFunc();
                groupcaptionsFunc();

                if ($stateParams.ProductId !== 0) {

                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomeproductstablealt/getincomeproduct/' + $stateParams.ProductId, null,
                        function (result) {
                            vm.clist = true;
                            vm.ipt = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.cedit = true;
                    vm.ipt = {
                        ProductCode: '', ProductName: '', Caption: '', Category: '',
                        Category_Filter: '', PPR_Status: '', Currency: '', VolumeGL: '',
                        RevGL: '', ExpGL: '', APR_Status: '', PPR_Caption: '', Position: '', 
                        Cash_Reserve_Item: '', Pool_Type: '', PL_Caption: '', Cash_Vault_Item: '',
                        moduleownertype: '', GroupCaption: '', Liquidity_Reserve_Item: '',
                        GCapPosition: '', SubTotal_item: '', Active: true                                              
                    };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {           

            ////Validate
            validator.ValidateModel(vm.ipt, iptRules);
            vm.viewModelHelper.modelIsValid = vm.ipt.isValid;
            vm.viewModelHelper.modelErrors = vm.ipt.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/incomeproductstablealt/updateincomeproduct', vm.ipt,
               function (result) {

                   $state.go('mpr-incomeproductstablealt-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
          }
           else {
               vm.viewModelHelper.modelErrors = vm.scm.errors;

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
                vm.viewModelHelper.apiPost('api/incomeproductstablealt/deleteincomeproduct', vm.ipt.ProductId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomeproductstablealt-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomeproductstablealt-list');
        }

        var captionsFunc = function () {
            vm.viewModelHelper.apiGet('api/caption/getallcaptions', null,
                function (result) {
                    vm.captionsList = result.data;
                },
                function (result) {
                    toastr.error('Fail to load users.', 'Fintrak');
                }, null);
        }

       
        //var plCaption2Func = function () {
        //    vm.viewModelHelper.apiGet('api/plcaption2/getallplcaption2', null,
        //        function (result) {
        //            vm.plcaption2List = result.data;
                   
        //        },
        //        function (result) {
        //            toastr.error('Fail to load profit center definitions.', 'Fintrak');
        //        }, null);
        //}

        var incomeCaptionFunc = function () {
            vm.viewModelHelper.apiGet('api/incomecaptionposition/getallincomecaptionposition', null,
                function (result) {
                    vm.incomecaptionList = result.data;

                },
                function (result) {
                    toastr.error('Fail to load profit center definitions.', 'Fintrak');
                }, null);
        }

        var pprCaptionFunc = function (code) {

            vm.viewModelHelper.apiGet('api/pprcaption/getallpprcaptions', null,
                function (result) {
                    vm.pprcaptionList = result.data;
                },
                function (result) {
                    toastr.error('Fail to load profit centers.', 'Fintrak');
                }, null);
        }

        var groupcaptionsFunc = function () {
            vm.viewModelHelper.apiGet('api/groupcaptions/getallgroupcaptions', null,
                function (result) {
                    vm.groupcaptionsList = result.data;
                },
                function (result) {
                    toastr.error('Fail to load users.', 'Fintrak');
                }, null);
        }
      
        //app.service('Products', function () {
        //    this.Items = function () {
        //        // if we want can get data from database 
        //        product = { product: '', price: '' }
        //    };
        //    return this;
        //});


        setupRules();
        initialize();


    }
}());
