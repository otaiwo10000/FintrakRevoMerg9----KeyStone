/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAccountMISOverrideTEMPListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IncomeAccountMISOverrideTEMPListController]);

    function IncomeAccountMISOverrideTEMPListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'incomeaccountMISoverrideTEMP-list-view';
        vm.viewName = 'Income Account MIS Override';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.accountno = '';
        vm.icsprb = [];  

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomeaccountMISoverrideTEMP/availableincomeaccountMISoverride', null,
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
                vm.viewModelHelper.apiGet('api/incomeaccountMISoverrideTEMP/getincomeaccountMISoverrideusingaccountnumber/' + vm.accountno, null,
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
            vm.viewModelHelper.apiGet('api/incomeaccountMISoverrideTEMP/availableincomeaccountMISoverride', null,
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
