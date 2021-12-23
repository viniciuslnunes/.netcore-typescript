import { v4 } from 'uuid'

class SwitchTermPair {
  public readonly id: string;
  public oldTerm: string;
  public newTerm: string;

  constructor (oldTerm = '', newTerm = '') {
    this.oldTerm = oldTerm
    this.newTerm = newTerm
    this.id = v4()
  }
}

export { SwitchTermPair }
