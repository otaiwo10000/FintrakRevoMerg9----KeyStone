﻿/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeProductsTableTreasuryListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IncomeProductsTableTreasuryListController]);

    function IncomeProductsTableTreasuryListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'incomeproductstabletreasury-list-view';
        vm.viewName = 'Income Products Table Treasury';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.iptList = [];
        vm.searchvalue = null;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomeproductstabletreasury/getallincomeproducttabletreasury', null,
                   function (result) {
                       vm.iptList = result.data;
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
                if ($('#iptTable').length > 0) {
                    var exportTable = $('#iptTable').DataTable({
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
        }

        vm.iptLoad = function () {

            
            if (vm.searchvalue == "") {
                alert("Search box is empty");
                return
            }
            
            //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
            vm.viewModelHelper.apiGet('api/incomeproductstabletreasury/getincomeproducttabletreasuryusingsearch/' + vm.searchvalue, null,
                function (result) {
                    vm.iptList = [];
                    vm.iptList = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }


        vm.refreshIPT = function () {
            vm.iptList = [];
            vm.viewModelHelper.apiGet('api/incomeproductstabletreasury/getallincomeproducttabletreasury', null,
                function (result) {

                    vm.iptList = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };


        initialize();
        
    }
}());
