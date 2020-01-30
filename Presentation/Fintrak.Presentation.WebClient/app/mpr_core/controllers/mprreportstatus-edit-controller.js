/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MPRReportStatusEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', 'yearsService',
                        MPRReportStatusEditController]);

    function MPRReportStatusEditController($scope, $window, $state, $stateParams, viewModelHelper, validator, yearsService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'mprreportstatus-edit-view';
        vm.viewName = 'MPR Report Status';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.mprreportstatus = {};
        vm.yearList = [];
        vm.periodList  = [
            { id: 1, name: "January" },
            { id: 2, name: "February" },
            { id: 3, name: "March" },
            { id: 4, name: "April" },
            { id: 5, name: "May" },
            { id: 6, name: "June" },
            { id: 7, name: "July" },
            { id: 8, name: "August" },
            { id: 9, name: "September" },
            { id: 10, name: "October" },
            { id: 11, name: "November" },
            { id: 12, name: "December" }
        ];

        //$scope.getSelectAllPeriod = function (data) {
        //    var outpNew = "";
        //    angular.forEach(data, function (value, key) {
        //        //outpNew += value.Team_Code + ",";
        //        outpNew += value.id;
        //    });
        //    //vm.userMIS.ProfitCenterMisCode = outpNew;
        //    vm.mprreportstatus.Period = outpNew;
        //};

        vm.splitedPeriods = [];
        vm.selectedPeriodsLabel = null;
        $scope.getSelectAllPeriod = function (data) {
            var outpNew = "";
            var n = 0;
            angular.forEach(data, function (value, key) {
                //outpNew += value.id + ",";
                n = n + 1;
                if (n < data.length) { outpNew += value.id + ","; }
                else { outpNew += value.id; }
            });
            //vm.periodmodel = outpNew;
            vm.mprreportstatus.Period = outpNew;  //eg "3,1,2" is in outpNew
            vm.splitedPeriods = outpNew.split(',');
            periodFunc(vm.splitedPeriods);
            //vm.mprreportstatus.Period = vm.p.join('/');
            vm.selectedPeriodsLabel = vm.p.join('/');
        };

        vm.statusList = [{ value: 'off', name: 'off' }, { value: 'on', name: 'on' }];                      

        vm.init = false;
        vm.instruction = '';

        var mprreportstatusRules = [];

        var setupRules = function () {

            mprreportstatusRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            mprreportstatusRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));

            mprreportstatusRules.push(new validator.PropertyRule("Status", {
                required: { message: "Status is required" }
            }));

        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.MPRReportStatusId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/mprreportstatus/getmprreportstatus/' + $stateParams.MPRReportStatusId, null,
                        function (result) {
                            vm.mprreportstatus = result.data;

                            //initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.mprreportstatus = { Year: 0, Period: 0, Status: '', Active: true };
            }
        };


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.mprreportstatus, mprreportstatusRules);
            vm.viewModelHelper.modelIsValid = vm.mprreportstatus.isValid;
            vm.viewModelHelper.modelErrors = vm.mprreportstatus.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/mprreportstatus/updatemprreportstatus', vm.mprreportstatus,
                    function (result) {

                        $state.go('mpr-mprreportstatus-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.mprreportstatus.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        };

        //vm.delete = function () {
        //    var deleteFlag = $window.confirm(' Are you sure you want to delete');

        //    if (deleteFlag) {
        //        vm.viewModelHelper.apiPost('api/abcratio/deleteabcratio', vm.abcRatio.AbcRatioId,
        //      function (result) {
        //          toastr.success('Selected item deleted.', 'Fintrak');
        //          $state.go('mpr-abcratio-list');
        //      },
        //      function (result) {
        //          toastr.error(result.data, 'Fintrak');
        //      }, null);
        //    }
        //}

        vm.cancel = function () {
            $state.go('mpr-mprreportstatus-list');
        };


        var yearFunc = function () {
            yearsService.yearsFunc()
                .then(function (data) {
                    vm.yearList = data;
                    //alert(vm.yearList);               
                }).catch(function (result) {
                    alert("Got error");
                });
        };

        vm.p = [];
        // angular.forEach(vm.MTList_t2, function (value, key) {
        var periodFunc = function (peri) {
            vm.p = [];
            angular.forEach(peri, function (value, key) {

                switch (value) {
                    case '1':
                        vm.p.push("Jan");
                        break;
                    case '2':
                        vm.p.push("Feb");
                        break;
                    case '3':
                        vm.p.push("Mar");
                        break;
                    case '4':
                        vm.p.push("Apr");
                        break;
                    case '5':
                        vm.p.push("May");
                        break;
                    case '6':
                        vm.p.push("Jun");
                        break;
                    case '7':
                        vm.p.push("Jul");
                        break;
                    case '8':
                        vm.p.push("Aug");
                        break;
                    case '9':
                        vm.p.push("Sep");
                        break;
                    case '10':
                        vm.p.push("Oct");
                        break;
                    case '11':
                        vm.p.push("Nov");
                        break;
                    case '12':
                        vm.p.push("Dec");
                }
            });
        };



        yearFunc();
        setupRules();
        initialize();
    }
}());
