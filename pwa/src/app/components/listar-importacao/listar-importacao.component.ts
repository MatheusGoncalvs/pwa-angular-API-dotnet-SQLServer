import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Lote } from 'src/app/Models/Lote';
import { LoteTrackerError } from 'src/app/Models/LoteTrackerError';
import { LoteService } from 'src/app/services/lote.service';

@Component({
  selector: 'app-listar-importacao',
  templateUrl: './listar-importacao.component.html',
  styleUrls: ['./listar-importacao.component.css']
})
export class ListarImportacaoComponent implements OnInit {

  lotes: Array<Lote>;

  constructor(
    public route: Router,
    private loteService: LoteService
  ) { }

  ngOnInit() {
    this.loteService.obterLotes()
      .subscribe(
        instructors => this.setLotes(instructors),
        (error: LoteTrackerError) => this.showErrorMessage(error)
      );
  }

  setLotes(lotes) {
    this.lotes = lotes;
  }

  showErrorMessage(error) {
    console.log(`Error: ${error.friendlyMessage}`);
  }

  voltarPaginaInicial() {
    this.route.navigate([""]);
  }

}
