import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { Lote } from 'src/app/Models/Lote';
import { LoteTrackerError } from 'src/app/Models/LoteTrackerError';
import { LoteService } from 'src/app/services/lote.service';

@Component({
  selector: 'app-listar-importacao',
  templateUrl: './listar-importacao.component.html',
  styleUrls: ['./listar-importacao.component.css']
})
export class ListarImportacaoComponent implements OnInit, OnChanges {

  lotes: Array<Lote>;

  constructor(
    public route: Router,
    private loteService: LoteService
  ) { }

  ngOnChanges(changes: SimpleChanges): void {
    this.loteService.obterLotes()
      .subscribe(
        instructors => this.setLotes(instructors),
        (error: LoteTrackerError) => this.showErrorMessage(error)
      );
  }

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

  removerLote(id: number) {
    this.loteService.removerLote(id)
      .subscribe(
        lote => console.log(lote),
        (error: LoteTrackerError) => this.showErrorMessage(error)
      );
  }

  showErrorMessage(error) {
    console.log(`Error: ${error.friendlyMessage}`);
  }

  voltarPaginaInicial() {
    this.route.navigate([""]);
  }

}
