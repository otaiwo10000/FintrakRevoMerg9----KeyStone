/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAccountMisOverrideListController",
                    ['$scope', '$window', '$state', 'viewModelHelper', 'validator',
                        IncomeAccountMisOverrideListController]);

    function IncomeAccountMisOverrideListController($scope, $window, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomeaccountmisoverride-list-view';
        vm.viewName = 'Income Account Mis Override';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeaccountmisoverrides = [];
        vm.selectedId = '';
        $scope.selection = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.searchvalue1 = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomeaccountmisoverride/getincomeaccountmisoverride', null,
                   function (result) {
                       vm.incomeaccountmisoverrides = result.data;
                       InitialView();
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#incomeaccountmisoverridesTable').length > 0) {
                    var exportTable = $('#incomeaccountmisoverridesTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            }, 50);
        }

        vm.obuLoad = function () {

            //vm.icsprb.length = 0;
            //this.exportTable.clear();
            vm.incomeaccountmisoverrides = [];
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomeaccountmisoverride/getincomeaccountmisoverrideByParam/' + vm.searchvalue1, null,
                    function (result) {

                        vm.incomeaccountmisoverrides = result.data;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.refreshOBU = function () {
            vm.incomeaccountmisoverrides = [];
            vm.viewModelHelper.apiGet('api/incomeaccountmisoverride/getincomeaccountmisoverride', null,
                function (result) {

                    vm.incomeaccountmisoverrides = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        initialize();
    }
}());
