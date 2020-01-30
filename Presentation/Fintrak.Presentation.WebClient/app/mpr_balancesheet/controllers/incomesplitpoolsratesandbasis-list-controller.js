/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeSplitPoolsRatesAndBasisListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IncomeSplitPoolsRatesAndBasisListController]);

    function IncomeSplitPoolsRatesAndBasisListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'incomesplitpoolsratesandbasis-list-view';
        vm.viewName = 'Income Split PoolsRate and Basis';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.year = 0;
        vm.period = 0;
        vm.icsprb = [];  
        //vm.searchvalue = '';

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomesplitpoolsratesandbasis/availableincomesplitpoolsratesandbasis', null,
                    function (result) {
                        vm.icsprb = result.data;
                        InitialView();
                        vm.init === true;

                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };


        var InitialView = function () {
            InitialGrid();
        };

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#icsprbTable').length > 0) {
                    var exportTable = $('#icsprbTable').DataTable({
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
        };


        vm.icsprbLoad = function () {

            //vm.icsprb.length = 0;
            //this.exportTable.clear();
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomesplitpoolsratesandbasis/getincomesplitpoolsratesandbasisusingyearmonth/' + vm.year + '/' + vm.period, null,
                    function (result) {

                        vm.icsprb = result.data;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.refreshICSPRB = function () {
            vm.icsprb = [];
            vm.viewModelHelper.apiGet('api/incomesplitpoolsratesandbasis/availableincomesplitpoolsratesandbasis', null,
                function (result) {

                    vm.icsprb = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };



        initialize();

    }
}());
