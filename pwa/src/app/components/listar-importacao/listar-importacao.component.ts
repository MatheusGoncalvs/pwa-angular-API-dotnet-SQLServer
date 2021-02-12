import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { EMPTY, Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Lote } from 'src/app/Models/Lote';
import { LoteTrackerError } from 'src/app/Models/LoteTrackerError';
import { LoteService } from 'src/app/services/lote.service';

@Component({
  selector: 'app-listar-importacao',
  templateUrl: './listar-importacao.component.html',
  styleUrls: ['./listar-importacao.component.css']
})
export class ListarImportacaoComponent implements OnInit {

  lotes$: Observable<Lote[]>;

  errorMessage = '';

  constructor(
    public route: Router,
    private loteService: LoteService
  ) { }

  ngOnInit() {
    this.obterLotes();
  }

  obterLotes() {
    this.lotes$ = this.loteService.obterLotes()
      .pipe(
        catchError(err => {
          this.errorMessage = err;
          return EMPTY;
        })
      );
  }

  removerLote(id: number) {
    this.loteService.removerLote(id)
      .subscribe(
        success => this.obterLotes(),
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
