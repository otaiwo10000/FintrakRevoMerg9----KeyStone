/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCommFeeLineCaptionListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IncomeCommFeeLineCaptionListController]);

    function IncomeCommFeeLineCaptionListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomecommfeelinecaption-list-view';
        vm.viewName = 'Income Comm Fee Line Caption';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.icflList = [];
        vm.searchvalue = null;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomecommfeelinecaption/getallincomecommfeelinecaption', null,
                    function (result) {
                        vm.icflList = result.data;
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
                if ($('#icflTable').length > 0) {
                    var exportTable = $('#icflTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        "searching": false,
                        // "dom": 'lBfrtip',

                        "sDom": "T<'clearfix'>" +
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

        vm.icflLoad = function () {

            vm.icflList = [];
            if (vm.searchvalue === "") {
                vm.searchvalue = null;
                alert("Search box is empty");
                return;
            }

            //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
            vm.viewModelHelper.apiGet('api/incomecommfeelinecaption/getincomecommfeelinecaptionusingsearch/' + vm.searchvalue, null,
                function (result) {

                    vm.icflList = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };


        vm.refreshICFL = function () {
            vm.icflList = [];
            vm.viewModelHelper.apiGet('api/incomecommfeelinecaption/getallincomecommfeelinecaption', null,
                function (result) {

                    vm.icflList = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };


        initialize();
        
    }
}());
