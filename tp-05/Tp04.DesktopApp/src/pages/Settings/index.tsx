import React, { useCallback, useEffect, useState } from 'react'
import { FiSettings } from 'react-icons/fi'
import { AiOutlineCrown } from 'react-icons/ai'
import { BsBookmarkCheck } from 'react-icons/bs'
import { MdCompareArrows } from 'react-icons/md'

import { useSettings } from '../../hooks/settings'

import { validateNextTermPairsValue } from './functions/validateNextTermPairsValue'

import Input from '../../components/Input'
import NavBar from '../../components/NavBar'
import Button from '../../components/Button'

import { SwitchTermPair } from './entities/SwitchTermPair'

import * as SC from './styles'
import { BiBook, BiUser } from 'react-icons/bi'
import IBook from '../CheckOwner/interfaces/IBook'
import axios from 'axios'
import apiConfig from '../../config/api'
import { useHistory } from 'react-router'
import { routesMap } from '../../config/routes-map'
import { useLocation } from 'react-router-dom'

const Settings: React.FC = () => {
  const [bookName, setBookName] = useState('')
  const [bookPassword, setBookPassword] = useState('')
  const [bookStatus, setBookStatus] = useState(true)
  const [bookId, setBookId] = useState<number | null>(null)
  const history = useHistory()
  const queryParams = new URLSearchParams(useLocation().search)

  useEffect(() => {
    const bookIdParam = queryParams.get('userId')

    if (bookIdParam) {
      setBookId(Number(bookIdParam))

      alert('Segura ae que eu vou carregar o usuário, mano')

      axios
        .get<IBook>(`${apiConfig.baseUrl}/usuarios/${bookIdParam}`)
        .then(({ data }) => {
          setBookName(data.nome)
          setBookPassword(data.senha)
          setBookStatus(data.status)

          alert('Boa, segue aí')
        })
    }
  }, [])

  const handleSaveSettings = useCallback(async () => {
    const book: IBook = {
      id: bookId,
      nome: bookName,
      senha: bookPassword,
      status: bookStatus
    }

    const response = bookId
      ? await axios.put(`${apiConfig.baseUrl}/usuarios`, book)
      : await axios.post(`${apiConfig.baseUrl}/usuarios`, book)

    console.log(response)

    history.push(routesMap.Home.path)
  }, [bookName, bookPassword])

  return (
    <SC.Container>
      <NavBar />

      <SC.Title>
        <FiSettings size={32} />
      </SC.Title>

      <SC.Content>
        <SC.SettingsGroup>
          <strong className="header">Preenche aí</strong>

          <Input
            value={bookName}
            icon={BiBook}
            placeholder="Nome"
            onChange={(event) => setBookName(event.target.value)}
          />

          <Input
            value={bookPassword}
            icon={BiUser}
            placeholder="Senha"
            onChange={(event) => setBookPassword(event.target.value)}
          />
        </SC.SettingsGroup>

        <SC.Footer>
          <Button onClick={handleSaveSettings}>Salvar</Button>
        </SC.Footer>
      </SC.Content>
    </SC.Container>
  )
}

export default Settings
