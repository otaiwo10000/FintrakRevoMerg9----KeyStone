/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SCMEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$location',
                        SCMEditController]);

    function SCMEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'scorecardmetrics-edit-view';
        vm.viewName = 'MPR ScoreCard Metrics';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.ts = {};
        vm.scm = {};
        

        //vm.value1 = 'Checked'
        //vm.value2 = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var scmRules = [];
        vm.perspectives = [];
        vm.acctText = '';
        vm.defcode = [];
        vm.miscode = [];
        vm.definitioncode = '';

        var setupRules = function () {

            scmRules.push(new validator.PropertyRule("Metric", {
                required: { message: "Metric Name is required" }
            }));

            scmRules.push(new validator.PropertyRule("Metric_Code", {
                required: { message: "Metric Code is required" }
            }));

            scmRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));

            scmRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));          
        }

        var initialize = function () {
            if (vm.init === false) {

                perspective();
                getTeamDefinitions();

                if ($stateParams.MetricId !== 0) {

                    vm.acctText = 'A';
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/scorecardmetrics/getscorecardmetric/' + $stateParams.MetricId, null,
                   function (result) {

                       vm.scm = result.data;
                       //vm.misco(vm.scm.DefinitionCode);
                       //vm.aofficer(vm.scm.DefinitionCode);
                      // vm.misco(vm.definitioncode);
                      // vm.aofficer(vm.definitioncode);
                       //vm.acctText = vm.scm.MisCode;
                      // vm.definitioncode = window.location.definitioncode;
                    

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.scm = {
                        Metric: '', Metric_Code: '', Metric_Description: '', MisCode: '',
                        Metric_Score_determinant: '', Period: '', Year: '', PerspectiveId: '',
                        Metric_Position: '', Mapping_Code: '', Active: true                       
                    };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {           

            ////Validate
            validator.ValidateModel(vm.scm, scmRules);
            vm.viewModelHelper.modelIsValid = vm.scm.isValid;
            vm.viewModelHelper.modelErrors = vm.scm.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/scorecardmetrics/updatescorecardmetric', vm.scm,
               function (result) {

                   $state.go('mpr-scorecardmetrics-list');
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
                vm.viewModelHelper.apiPost('api/scorecardmetrics/deletescorecardmetric', vm.scm.MetricId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-scorecardmetrics-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-scorecardmetrics-list');
        }

        var perspective = function () {
            vm.viewModelHelper.apiGet('api/scorecardperspective/getallscorecardperspective', null,
                function (result) {
                    vm.perspectives = result.data;
                },
                function (result) {
                    toastr.error('Fail to load users.', 'Fintrak');
                }, null);
        }

        //var misco = function () {
        //    var code = 'ACCT';
        //    vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingdefcode/' + code, null,
        //        function (result) {
        //            vm.miscode = result.data;
        //        },
        //        function (result) {
        //            toastr.error('Fail to load users.', 'Fintrak');
        //        }, null);
        //}

        var getTeamDefinitions = function () {
            vm.viewModelHelper.apiGet('api/teamdefinition/availableteamdefinitions', null,
                function (result) {
                    vm.defcode = result.data;
                   
                },
                function (result) {
                    toastr.error('Fail to load profit center definitions.', 'Fintrak');
                }, null);
        }

        //vm.teamstructurebydefcode = function (code) {
        vm.misco = function (code) {

            vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingdefcode/' + code, null,
                //vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingsetup', null,
                function (result) {
                    vm.miscode = result.data;
                },
                function (result) {
                    toastr.error('Fail to load profit centers.', 'Fintrak');
                }, null);
        }

        vm.aofficer = function (code) {

            //vm.c = 'ACCT';
            //vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingparamsANDsetup/' + vm.c + '/' + vm.acctText, null,
            vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingparamsANDsetup/' + code + '/' + vm.acctText, null,
                function (result) {
                    vm.miscode = result.data;
                },
                function (result) {
                    toastr.error('Fail to load currencies.', 'Fintrak');
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
