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
  const [bookAuthor, setBookAuthor] = useState('')
  const [bookId, setBookId] = useState<number | null>(null)
  const history = useHistory()
  const queryParams = new URLSearchParams(useLocation().search)

  useEffect(() => {
    const bookIdParam = queryParams.get('bookId')

    if (bookIdParam) {
      setBookId(Number(bookIdParam))

      alert('Segura ae que eu vou carregar o livro, mano')

      axios
        .get<IBook>(`${apiConfig.baseUrl}/livros/${bookIdParam}`)
        .then(({ data }) => {
          setBookName(data.titulo)
          setBookAuthor(data.autor)

          alert('Boa, segue aí')
        })
    }
  }, [])

  const handleSaveSettings = useCallback(async () => {
    const book: IBook = {
      id: bookId,
      titulo: bookName,
      autor: bookAuthor
    }

    const response = bookId
      ? await axios.put(`${apiConfig.baseUrl}/livros`, book)
      : await axios.post(`${apiConfig.baseUrl}/livros`, book)

    console.log(response)

    history.push(routesMap.Home.path)
  }, [bookName, bookAuthor])

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
            placeholder="Título"
            onChange={(event) => setBookName(event.target.value)}
          />

          <Input
            value={bookAuthor}
            icon={BiUser}
            placeholder="Autor"
            onChange={(event) => setBookAuthor(event.target.value)}
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
