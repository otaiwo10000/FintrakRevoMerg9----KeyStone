/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MISTransferPriceListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        MISTransferPriceListController]);

    function MISTransferPriceListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'mistransferprice-list-view';
        vm.viewName = 'MIS Transfer Price';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.mistp = [];
        vm.defCode = ''; vm.miscode = ''; vm.category = ''; vm.currency = ''; vm.period = ''; vm.year = '';
      

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/mistransferprice/getmistransferpriceusingsetup', null,
                    function (result) {
                        vm.mistp = result.data;
                       InitialView();
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        vm.mistpLoad = function () {

            vm.mistp = [];
            //if (vm.searchvalue === "") {
            //    alert("Search box cannot be empty");
            //}

            //else if (vm.year === "") {
            //    alert("Search with year cannot be empty")
            //    return
            //}

            //else {
                if (vm.init === false) {
                    //vm.ts = []; 
                    //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
                    vm.viewModelHelper.apiGet('api/mistransferprice/getmistransferpriceusingparams/' + vm.defCode + '/' + vm.miscode + '/' + vm.category + '/' + vm.currency + '/' + vm.period + '/' + vm.year, null,
                        function (result) {
                            
                            vm.mistp = result.data;
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
           // }
        }

       

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#mistpTable').length > 0) {
                    var exportTable = $('#mistpTable').DataTable({
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

        vm.refreshMISTP = function () {
            initialize();
        };

        initialize();

    }
}());
