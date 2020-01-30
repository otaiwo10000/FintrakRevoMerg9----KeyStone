/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCustomerRatingOverrideTEMPListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IncomeCustomerRatingOverrideTEMPListController]);

    function IncomeCustomerRatingOverrideTEMPListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'incomecustomerratingoverrideTEMP-list-view';
        vm.viewName = 'Income Customer Rating Override';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.referencenumber = '';
        vm.icro = [];  

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomecustomerratingoverrideTEMP/availableincomecustomerratingoverride', null,
                    function (result) {
                        vm.icro = result.data;
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
                if ($('#icroTable').length > 0) {
                    var exportTable = $('#icroTable').DataTable({
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


        vm.icroLoad = function () {

            //vm.icsprb.length = 0;
            //this.exportTable.clear();
            vm.icro = [];
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomecustomerratingoverrideTEMP/getoverridebyrefnumber/' + vm.referencenumber, null,
                    function (result) {

                        vm.icro = result.data;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.refreshICRO = function () {
            vm.icro = [];
            vm.viewModelHelper.apiGet('api/incomecustomerratingoverrideTEMP/availableincomecustomerratingoverride', null,
                function (result) {

                    vm.icro = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };



        initialize();

    }
}());
